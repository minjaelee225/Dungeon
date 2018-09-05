# MysteryDungeon
This is the repository for the dungeon crawler game

Git Instructions

NOTE: Wherever double quotes appear, please use the name of your choice WITHOUT the quotes

1) Make an empty directory in your folder of choice, using mkdir "YOUR FOLDER NAME"
2) Enter the folder using cd "YOUR FOLDER NAME"
3) Add the remote: git remote add origin https://github.com/minjaelee225/Dungeon
4) Enter your credentials
5) Pull code from the remote: git pull origin master

Congratulations! You should now have your project code. To work on the project and make changes, it is recommended you switch to a new branch. The following instructions go over that process:

1) Make a new branch: git branch "YOUR BRANCH NAME". You can check existing branches with the command git branch
2) Switch to the newly created branch: git checkout "YOUR BRANCH NAME".

Now, you can make changes on your local machine to your project code. To commit changes, use the usual commands:

1) Add the necessary files. For convenience, use git add * This adds all the files in the project folder. Otherwise, add each file with gid add "YOUR FILE NAME".
2) Commit your changes: git commit -a If you'd like to add a message, use git commit -m "YOUR MESSAGE HERE" (NOTE: This time you DO need the double quotes)
3) Push your code to the origin: git push origin master.

REMEMBER TO SWITCH TO THE MASTER BRANCH TO PULL THE CODE FROM THE REPO. WHEN YOU ARE WORKING ON YOUR LOCAL MACHINE, SWITCH BACK TO THE BRANCH YOU CREATED.

Merging branches

You can merge branches with the following command:

1) git merge "YOUR BRANCH NAME"
2) Check for differences: git diff If there are conflicts between the branches, the files affected will be shown.
3) Commit as usual: git commit -a
4) You can now delete your branch: git branch -d "YOUR BRANCH NAME"
