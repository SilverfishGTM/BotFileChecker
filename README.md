***********************
Bot File Checker V1.0.4
***********************
Made by Silverfish
Source code should have been an option to include with your download, program was made in visual studio C#. If you don't want to trust the author, you can compile the code yourself.
***********************
Special thanks to:
cephalopod, Geese, KILLER, TheRoboteer, Nabi
for test bots, guides, bug reports, and suggestions
***********************
This program isn't perfect, and may screw up your computer. This is the same for every program ever written. Run it at your own risk.
Talk to Silverfish if you find a bug.
***********************
How to use:
Fill out the blanks (or click the browse button and select) for:
Location of the .bot file you want to check.
Folder of your 'Robot Arena 2.exe' file (so that the program can find component .txts).
Output folder (the program will dump a detailed log here as well as any component lists you'll want to generate).
Location of your armor definitions file, if you want armor checked.

The check boxes are:
Is this an extenderbot? - If this is checked, the program will log any components after ID #0 that are attached to the chassis (excepting smartzones).
Generate Component List - Causes the program to generate a list of all the components the bot uses.

Once you have your settings as you want them, click the 'Check File' button.
***********************
How to interpret the results:
Initial screen:
You will get a pop-up that tells you how many 'critical edits' and how many 'warnings' the program ran across. 'Critical edits' are changes that are.
basically never legal, such as armor modifications, component type changes, and components BFEd to the chassis of an extenderbot.
'Warnings' for now just means cheatbot2 components and DSA armor, this may change as the program (hopefully) gets updated.

Logs/component list:
Going to your output folder, the program will have generated one to two files: log.txt and possibly components.txt.
log.txt contains a list of the critical edits (beginning with "CRITICAL:") and warnings (beginning with "WARNING:") that the program has found.
components.txt contains a list of the components used in the building of the bot, in the format:
<component's display name>(<component's txt path>): <number of this component present in the bot>
************************
Errors:
In the event of an error, a box will pop up that begins with "ERROR:" and is followed by an error message. Some errors will also write into log.txt.
If you get one of these, make sure that your locations are right (and that you have all the prerequisite components), and then talk to Silverfish if that doesn't work.
************************
What this program checks for:
This program should, at current, be able to find:
-Modified component types (normal components being listed as weapon so that they do damage, for example)
-cheatbot2 components.
-Armor modifications, if you have a file listing the selectable armors in the style of DSL 2.4's armor_definitions.txt (decreased weight/increased strength).
-Components being BFEd to the chassis of an extenderbot.
************************
What this program DOESN'T check for:
This program isn't a replacement for actually looking at a bot in the bot lab. Here are some examples of what this program WON'T tell you about:
-Stacked components (you can get a list of all the components, but it won't tell you if they're stacked).
-Cheating custom components (this could hypothetically be a seperate program, but I haven't written it and probably won't).
-Armor modifications if you don't have a file (you'll need a custom armor definitions file in the style of the one that comes with DSL 2.4).
-Clipping components (too much eyeballing is necessary to reasonably do this).
-Miscellaneous IRL ruleset violations (I.E. more components than are allowed, weapons on rammers, etc.).
-Cheaty .pys and AI lines (eternalflame, for instance).
