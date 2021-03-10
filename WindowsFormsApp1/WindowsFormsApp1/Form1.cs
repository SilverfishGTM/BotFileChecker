/* Bot file checker v1.0.4
 * @author: Silverfish
 * @special thanks: cephalopod, Geese, KILLER, TheRoboteer, Nabi 
 * READ THE README IF YOU DON'T UNDERSTAND WHAT TO DO/HOW TO READ THE RESULTS.
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //These variables will be used by multiple functions
        bool isExtenderBot; //Flag for if the bot is an extender bot
        bool genComponentList; //Flag for whether to generate a component list
        string botPath; //The path of the .bot file to check
        string RA2Path; //The directory in which Robot Arena 2.exe exists
        string outPath; //The directory to print logs to
        string armorPath; //The location of the armor definitions file
        string lastBotPath; //The last folder that the user picked a bot file in
        string lastArmorPath; //The last folder the user picked an armor definitions file in
        int criticalCount; //Current number of critical errors
        int warningCount; //Current number of warnings
        List<string> cheatbotStyles; //This contains a list of the children of all the cheatbot components.
        const string DASHES = "-----------------";

        OpenFileDialog fileDiag = new OpenFileDialog();
        FolderBrowserDialog folderDiag = new FolderBrowserDialog();


        public Form1()
        {
            InitializeComponent(); //This is a default line for loading a visual studio program.

            //Set up the program by reading in the values.
            isExtenderBot = ExtenderbotBox.Checked;
            genComponentList = ComponentListBox.Checked;
            botPath = BotDirectoryBox.Text;
            RA2Path = RA2DirectoryBox.Text;
            fileDiag.InitialDirectory = "c:\\";
            outPath = OutputPathBox.Text;
            criticalCount = 0;
            warningCount = 0;
            armorPath = ArmorPathBox.Text;
            lastArmorPath = "";
            lastBotPath = "";
        }

        //Check File Button
        private void button1_Click(object sender, EventArgs e)
        {
            if(botPath == "") //This implies nothing was put in the bot file path box
            {
                MessageBox.Show("No bot path was supplied. Please enter a bot path and try again.");
                return;
            }
            if(RA2Path == "") //This one is for RA2 directory
            {
                MessageBox.Show("No RA2 path was supplied. Please enter the directory of your Robot Arena 2.exe file and try again.");
                return;
            }
            if(outPath == "") //Output
            {
                MessageBox.Show("No output path was supplied. Please enter a directory for this program to put log files and try again.");
                return;
            }
            if(armorPath == "") //And lastly armor. Armor defs aren't REQUIRED for the program to run, so we just make sure the user knows.
            {
                MessageBox.Show("Warning: No armor path was supplied. Armor will not be checked for edits.");
            }

            try
            {
                //Open up the bot file and make the log
                StreamReader botFile = File.OpenText(botPath);

                //Get our list of cheatbot children ready
                cheatbotStyles = TrawlComponents();

                //Reset the error counts so that we don't have them carrying over from the last instance.
                criticalCount = 0;
                warningCount = 0;

                int temp; //This is going to be used for tryParse to make sure a string is a number.

                string currentLine = ""; //The line the program has just read
                string previousLine = ""; //The line before currentLine
                string componentFlag = ""; //Qualifier used to ID component type
                bool componentNext = false; //This is used to signal a component path being the next line in the file.
                string currentComponent = ""; //The name (and path) of the last component the program read
                int listLength = 0; //The number of uniquie components used on the bot
                bool pathLast = false; //This is a flag that tells us if the last line was the path for a component txt
                string armorLine = ""; //This will be used to save the armor value line
                bool firstCompFound = false; //Flag for if we've reached the 'components' section
                bool firstCheckComplete = false; //Flag for whether the armor has been checked yet (given that it will be)
                bool nextCompOnChassis = false; //This is a flag set when the next component on an extenderbot is directly attached to the chassis.

                if(armorPath == "")
                {
                    firstCheckComplete = true; //If there's no armor definitions file, don't bother checking armor.
                }

                botFile.ReadLine();

                string botName = botFile.ReadLine().Substring(6); //This will be used to name log files.
                StreamWriter log = File.CreateText(outPath + @"\" + botName + "-log.txt"); //Append the bot's name to the log.


                log.WriteLine("Beginning log...");

                List<string> componentsUsed = new List<string>(); //A list of all the unique components used
                List<int> componentNumbers = new List<int>(); //A list of the number of each component used (runs parallel to componentsUsed

                while ((currentLine = botFile.ReadLine()) != null) //While there's still data left in the bot file
                {
                    if(firstCompFound && !firstCheckComplete) //If we've reached the components section and haven't checked the armor
                    {
                        ArmorScan(armorPath, armorLine, log);
                        firstCheckComplete = true;
                    }

                    if (componentNext) //If the last line set this flag to true, it means we have a component that needs scanning
                    {
                        if (componentsUsed.Contains(currentComponent = ScanComponent(currentLine, componentFlag, log))) //ScanComponent returns the component's name, so we can check this against our list of unique components.
                        {
                            componentNumbers[componentsUsed.IndexOf(currentComponent)]++; //If the component has been used before, add 1 to our count for it
                        }
                        else if (currentComponent != "") //The value "" would return if the component scanner ran into a problem.
                        {
                            componentsUsed.Add(currentComponent); //If it hasn't, add the component to the list
                            componentNumbers.Add(1);
                            listLength++;
                        }
                        else
                        {
                            log.WriteLine("ERROR: Reader had an issue scanning a component."); //Handle the reader having an issue
                        }

                        if(isExtenderBot && nextCompOnChassis)
                        {
                            if (componentFlag != "SmartZone")
                            {
                                log.WriteLine(DASHES);
                                log.WriteLine("CRITICAL: Component " + currentComponent + " has been BFEd to the chassis of this extenderbot!");
                                criticalCount++;
                            }
                            nextCompOnChassis = false; //Write in an issue with components attached to the chassis and reset the flag for it.
                        }

                        componentNext = false; //We just finished handling a component, this flag is no longer true
                        pathLast = true; //However, the last line (the one we just read) was a path.
                    }
                    else if(pathLast) //If we just finished handling a component
                    {
                        if(isExtenderBot && currentLine.Equals("0")) //If the new component is attached to the chassis and we're looking at an extenderbot
                        {
                            nextCompOnChassis = true; //Set a flag so that we can say the next component is attached to the chassis.
                        }

                        if (!currentLine.Contains(" ") && int.TryParse(currentLine, out temp)) //Burst motors in particular have range of motion directly below the path, but before the base component for the next component. We need to handle this, and the burst line would need to contain a space. Additionally, smartzones use words, so parsing an int here should fix the issue.
                            pathLast = false; //If we didn't just read a burst motor line, we can move on.
                    }
                    //This line checks to see if we have a component type identifier as the current line
                    else if (currentLine.Equals("Component") || currentLine.Equals("Weapon") || currentLine.Equals("Wheel") || currentLine.Equals("Battery") || currentLine.Equals("SpinMotor") || currentLine.Equals("BurstMotor") || currentLine.Equals("ControlBoard") || currentLine.Equals("SmartZone") || currentLine.Equals("ServoMotor") || currentLine.Equals("Flamethrower") || currentLine.Equals("Magnet") || currentLine.Equals("Hovercraft") || currentLine.Equals("Cannon"))
                    {
                        if(!firstCompFound)
                        {
                            firstCompFound = true; //If we haven't already found the first component, this means we just did.
                        }
                        componentFlag = currentLine; //Set the component's type to the current line (because that's what the current line is)
                        componentNext = true; //Make sure to handle this component on the next pass.
                    }
                    else if(!firstCompFound && CharCount(currentLine, ' ') == 4 && CharCount(previousLine, ' ') == 4) //The armor line is always the second to last line with 4 spaces before the first component. (yes, that's weird)
                    {
                        armorLine = previousLine;
                    }

                    previousLine = currentLine; //The next command will be incrementing the current line, so get the previous line and set it to the current one.

                }

                if (genComponentList) //Only run this if we're supposed to generate a list of components.
                {
                    //Make a new file to store the list
                    StreamWriter componentList = File.CreateText(outPath + @"\" + botName + "-components.txt");

                    componentList.WriteLine("Beginning component list...");

                    for (int i = 0; i < listLength; i++) //Check for as many unique components as we have
                    {
                        componentList.WriteLine(componentsUsed[i] + ": " + componentNumbers[i] + "\n"); //Write the name and number for each.
                    }

                    componentList.WriteLine("End component list.");
                    componentList.Close(); //Close the new file.
                }

                log.WriteLine("End log.");

                //Give a pop up to the user showing the number of critical errors and warnings. This also doubles to show that the scan has finished.
                MessageBox.Show("Bot scan finished. There were " + criticalCount + " critical errors and " + warningCount + " warnings.");

                //Close the remaining files.
                log.Close();
                botFile.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show("ERROR: " + err.Message); //If an error occurs somewhere, show the user.
            }
            
        }


        //****************** HELPER FUNCTIONS *********************
        /*Checks a component file for cb2 and type mismatches
         *@var path: the .txt path of the component file
         *@var flag: the component type flag that the bot has listed for this component ("Component," "Battery," "Weapon," etc.)
         *@var log: the log's variable so that the function can write to it
         *@return: the components name and path in a formatted style.
         */
        private string ScanComponent(string path, string flag, StreamWriter log)
        {
            string currentFlag = ""; //Flag the component has listed for itself
            string currentLine = ""; //Current line we're reading in the file
            string componentName = ""; //Component's listed name
            bool isChild = path.Contains(@"Styles\"); //if the path contains the "styles" directory, we know that it's a child.

            try
            {
                StreamReader compFile = File.OpenText(RA2Path + @"\" + path); //Open the component file

                while ((currentLine = compFile.ReadLine()) != null) //While there's still data to read.
                {
                    if (currentLine.Length >= 4 && currentLine.Substring(0, 4).ToLower().Equals("name")) //If we've found the 'name' section of the file
                    {
                        componentName = currentLine.Substring(currentLine.IndexOf("=") + 2); //Save the component's name. We need to remove the 'name = ' part of the line.
                    }
                    else if (currentLine.Length >= 4 && currentLine.Substring(0, 4).ToLower().Equals("base")) //If we've found the component type section
                    {
                        currentFlag = currentLine.Substring(currentLine.IndexOf("=") + 2); //Save the type the component says it is
                        if (currentFlag.ToLower().Equals(flag.ToLower()) == false)
                        {
                            log.WriteLine(DASHES);
                            log.WriteLine("CRITICAL: Base type mismatch detected with file " + path + "!");
                            log.WriteLine("Component type is: " + currentFlag + ", Bot lists it as: " + flag + "!");
                            criticalCount++; //Throw a fit if the provided component type doesn't match the type the component lists for itself
                        }
                    }
                    else if (currentLine.Length >= 6 && currentLine.Substring(0, 6).ToLower().Equals("hidden")) //This entails a potential cheatbot component.
                    {
                        if (currentLine.Substring(currentLine.IndexOf("=") + 2).Equals("2"))
                        {
                            log.WriteLine(DASHES);
                            log.WriteLine("WARNING: Cheatbot component detected! File " + path + " has 'hidden' flag set to 2.");
                            log.WriteLine("Is this component legal in your tournament?");
                            warningCount++; //If hidden = 2 (cheatbot2 component), throw a fit
                        }
                    }
                }

                if (isChild) //We don't need to check if a component's parent is a cheatbot component if the component doesn't have parents.
                {
                    foreach (string element in cheatbotStyles) //This loop is going to be used to check our component name against cheatbot stuff.
                    {
                        if (element.Contains(path.Substring(path.LastIndexOf(@"\") + 1))) //Keep in mind, we're tracking the entire styles lines here.
                        {
                            log.WriteLine(DASHES);
                            log.WriteLine("WARNING: Cheatbot component detected! File " + path + "'s parent component has hidden = 2 (cheatbot component)");
                            log.WriteLine("Is this component legal in your tournament?");
                            warningCount++; //If a parent has this txt as a cheatbot child, throw a fit.
                            break;
                        }
                    }
                }
                

                compFile.Close(); //Close the file because we're done here.
            }
            catch (System.IO.DirectoryNotFoundException) //This means we were provided an invalid file path.
            {
                MessageBox.Show("ERROR: File " + path + " was not found.\n" +
                    "Check that you have the correct RA2 folder, then make sure you have the prerequisite components.");
            }
            catch (Exception err)
            {
                MessageBox.Show("ERROR: " + err.Message); //Catchall for any other error that may occur.
            }

            if (componentName != "") //If an error hasn't happened
                return componentName + "(" + path + ")";
            else
                return ""; //If an error occurred.
        }

        /* Checks a provided armor line to see if it's valid
         * @var path: the path to the armor definitions file
         * @var armorLine: the armor values line given by the bot
         * @var log: the log's variable so that the function can write to it
         */
        private void ArmorScan(string path, string armorLine, StreamWriter log)
        {
            if (isExtenderBot && armorLine.Equals("0 3 0 173 0")) //This is the armor line for the pixel chassis which is used on extenderbots. We don't need to do anything if these match.
            { }
            else if(armorLine.Equals("1 12 0 200 0"))
            {
                log.WriteLine(DASHES);
                log.WriteLine("WARNING: This robot uses Double Strength Aluminum (DSA) Armor!");
                log.WriteLine("Is this legal in your tournament?");
                warningCount++;
            }
            else
            {
                try
                {
                    StreamReader armorFile = File.OpenText(path); //Open the armor definitions file

                    string currentLine = ""; //Current line in the armor definitions file
                    bool armorError = true; //Assume this is true until we find a valid armor type that the given armor matches.

                    //Ok, here comes some weird string cutting garbage, so let's go.
                    int firstSpaceIndex = armorLine.IndexOf(" ");
                    int secondSpaceindex = armorLine.IndexOf(" ", firstSpaceIndex + 1);
                    int thirdSpaceIndex = armorLine.IndexOf(" ", secondSpaceindex + 1);
                    int fourthSpaceIndex = armorLine.IndexOf(" ", thirdSpaceIndex + 1); //This command and the three above it get the index of every space in the armor line.

                    if (firstSpaceIndex == 0 || secondSpaceindex == firstSpaceIndex + 1 || thirdSpaceIndex == secondSpaceindex + 1 || fourthSpaceIndex == thirdSpaceIndex + 1) //This means that there wasn't another space, meaning that the string is wrong.
                    {
                        MessageBox.Show("ERROR: Armor line was misread. This may be a strange bot file, you'll need to manually check this.");
                        return;
                    }

                    int armorType = int.Parse(armorLine.Substring(0, 1)); //This simply takes the first character, which will always be the armor type.
                    int armorWeight = int.Parse(armorLine.Substring(firstSpaceIndex + 1, secondSpaceindex - firstSpaceIndex)); //This gets the second string in the line, the armor weight
                    int armorStrength = int.Parse(armorLine.Substring(thirdSpaceIndex + 1, fourthSpaceIndex - thirdSpaceIndex)); //This gets the fourth string in the line, the armor strength

                    string newArmorLine = armorType.ToString() + " " + armorWeight.ToString() + " 0 0 " + armorStrength.ToString(); //This is how armor is formatted in the armor definitions file.

                    while ((currentLine = armorFile.ReadLine()) != null)
                    {
                        if (currentLine.Equals(newArmorLine))
                        {
                            armorError = false; //If we find a matching line for our current armor, this means we have a selectable armor type.
                        }
                    }

                    if (armorError)
                    {
                        log.WriteLine(DASHES);
                        log.WriteLine("CRITICAL: Armor stats don't line up!");
                        log.WriteLine("Armor line is: " + newArmorLine + ", this is not a selectable armor!");
                        criticalCount++; //If we didn't find a match for our armor, throw a fit.
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Invalid armor file path. Check your path again."); //This means an invalid armor file was supplied.
                }
                catch (Exception err)
                {
                    MessageBox.Show("ERROR: " + err.Message); //Catchall for any other errors.
                }
            }
        }

        /* Counts the number of occurrences of a particular char in a string.
         * @var test: the string to test for the char.
         * @var target: the target character to look for
         * @return: the number of instances of the target char.
         */
        private int CharCount(string test, char target)
        {
            int count = 0;
            char[] compArray = test.ToCharArray();

            for (int i = 0; i < test.Length; i++)
            {
                if (compArray[i] == target)
                {
                    count++;
                }
            }
            return count;
        }

        private List<string> TrawlComponents()
        {
            StreamReader currentFile;
            List<string> styles = new List<string>(); //This will contain a list of all the cheatbot components' styles
            string[] fileNames = Directory.GetFiles(RA2Path + @"\Components", "*.txt"); //Every text file in the components folder
            string localStyles = ""; //This'll be used to hold styles while we wait and see if the file is hidden
            bool isCheatbot = false; //Take a wild guess
            bool hasStyles = false; //If a component doesn't have styles, we don't need to track it
            string currentLine = ""; //Our current line in the file.

            for(int i = 0; i < fileNames.Length; i++)
            {
                isCheatbot = false;
                hasStyles = false;
                currentFile = File.OpenText(fileNames[i]);
                while((currentLine = currentFile.ReadLine()) != null)
                {
                    if(currentLine.Length >= 6 && currentLine.Substring(0,6).ToLower().Equals("styles")) //If we're in the styles line
                    {
                        if (currentLine.Length >= 16 && currentLine.ToLower().Contains(".txt")) //Because styles=default is a line.
                        {
                            localStyles = currentLine; //Save our styles
                            hasStyles = true;
                        }
                    }
                    else if(currentLine.Length >= 6 && currentLine.Substring(0,6).ToLower().Equals("hidden")) //If a hidden line exists
                    {
                        if (currentLine.Substring(currentLine.IndexOf("=") + 2).Equals("2")) //This means that we have a cheatbot component.
                        {
                            isCheatbot = true;
                        }
                    }

                    if (isCheatbot && hasStyles && !fileNames[i].Contains("IRLARM") && !fileNames[i].Contains("IRLEXT"))
                    {
                        styles.Add(localStyles); //Thow in our styles as cheatbot components if the parent is a cheatbot.
                    }
                }
                currentFile.Close();
            }
            return styles;
        }

        private void BotBrowseButton_Click(object sender, EventArgs e)
        {
            if (lastBotPath != "")
                fileDiag.InitialDirectory = lastBotPath;

            fileDiag.Filter = "Bot files (*.bot) | *.bot"; //Make it so that we can only read .bot files.
            if(fileDiag.ShowDialog() == DialogResult.OK) //If we returned successfully, change the bot path
            {
                botPath = fileDiag.FileName;
            }
            BotDirectoryBox.Text = botPath; //Update the text box.
            lastBotPath = botPath.Substring(0, botPath.LastIndexOf(@"\") + 1);
        }

        private void RA2BrowseButton_Click(object sender, EventArgs e)
        {
            folderDiag.ShowNewFolderButton = false; //We're looking for an existing foler here, not making a new one.
            if(folderDiag.ShowDialog() == DialogResult.OK) //If a folder was selected, change the directory path.
            {
                RA2Path = folderDiag.SelectedPath;
            }
            RA2DirectoryBox.Text = RA2Path; //Update the text box
        }

        private void OutputBrowseButton_Click(object sender, EventArgs e)
        {
            folderDiag.ShowNewFolderButton = true; //User may want to make a new folder for output
            if (folderDiag.ShowDialog() == DialogResult.OK)
            {
                outPath = folderDiag.SelectedPath;
            }
            OutputPathBox.Text = outPath; //Everything else is the same as RA2BrowseButton
        }

        //This is the armor definition file's browse button
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (lastArmorPath != "")
                fileDiag.InitialDirectory = lastArmorPath;

            fileDiag.Filter = "Text files (*.txt) | *.txt"; //Make it so that we can only read .txt files.
            if (fileDiag.ShowDialog() == DialogResult.OK) //If we returned successfully, change the armor definitions path
            {
                armorPath = fileDiag.FileName;
            }
            ArmorPathBox.Text = armorPath; //Update the text box.
            lastArmorPath = armorPath.Substring(0, armorPath.LastIndexOf(@"\") + 1);
        }

        //This is the RA2 Directory textbox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RA2Path = RA2DirectoryBox.Text; //Update the local variable for RA2's path if the text box changes.
        }

        private void BotDirectoryBox_TextChanged(object sender, EventArgs e)
        {
            botPath = BotDirectoryBox.Text;
        }

        private void OutputPathBox_TextChanged(object sender, EventArgs e)
        {
            outPath = OutputPathBox.Text;
        }

        private void ArmorPathBox_TextChanged(object sender, EventArgs e)
        {
            armorPath = ArmorPathBox.Text;
        }

        private void ExtenderbotBox_CheckedChanged(object sender, EventArgs e)
        {
            isExtenderBot = ExtenderbotBox.Checked;
        }

        private void ComponentListBox_CheckedChanged(object sender, EventArgs e)
        {
            genComponentList = ComponentListBox.Checked;
        }


        //The two functions below just need to exist.
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
