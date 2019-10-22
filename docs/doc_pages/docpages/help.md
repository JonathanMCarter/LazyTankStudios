@mainpage Homepage

Welcome to the Tales of Orelia Code Documentation!

@section tutorial1 Introduction
You will firstly need to download Doxygen .exe and install it on your system. Get it from here: http://www.doxygen.nl/download.html

Once installed you will need to run the Doxygen Wizard and set it up for the project. There are a few things to note here. The documentation you are seeing right now is in the master branch of the github repo under the /docs directory. This is where github pages generates the webpage that you are currently on. any updates you make will go into a different directory which you define. You will need to push any documentation updates into the master branch otherwise it will not update. However! Your scripts do not need to be here for this to work as Doxygen makes a html page with all the info on it. Hope that makes sense...

@section turotial2 How to Setup Doxygen
Once you've downloaded and installed Doxygen you'll need to run the doxygen wizard program.

Once open you'll need to follow the following steps:
1. In the **Step 1** directory, set it to be where the GameProject is. the directory should end with "\GameProject" if setup right.
2. Download the file "Doxyfile" from discord and open it in Doxygen under File -> Open, this will update the details for the webpage.
3. Setup the source code directory, This should be setup to go to the "\Assets\Scripts" folder in the GameProject. Make sure it has the C:/... otherwise it may not work! You can also do this step with Editor or Shaders if needed under Expert tab -> Input -> INPUT.
4. Tick the scan recursiley box (if you don't it won't find anything but whats in the directory).
5. Set an output location for the documentation, this can legit be anywhere you like, as long as it you can access it.

Once all thats done your ready to test it out. Go to the Run tab and press Run Doxygen, after a few seconds it should produce a "***Doxygen has finished" line in the console.
Once that has happened you can either go the output director and open the index.html or just press the Show HTML Output button. 

@section tutorial3 How do I format my scripts with content that comes up here?
To edit the pages you need to add comments with the "///" in front of it. This will mark it as a comment for Doxygen to use. 

To make a quick description of a class, just use the /// and write a sentance, as soon as you use a full stop it will end the quick description.
to just write stuff. just use the /// and type away, if typing after the quick description, the text your write will display undernear as the detailed description. 

Below as a few handy things you can add 

``` ###<text>``` //This will make a H3 header, add or remove #'s to make bigger or smaller headers.
###This is a header H3

``` ///@param <param> <text> ``` //Will let you describe the params of a function.
@param Hi This is a param.

``` ///@return <param> <text> ``` //Will let you describe the returns of a function.
@return Hi This is returned

``` ///@see <function> ``` //Will let you link to another function in the same script
@see DocumentationCheatSheet

``` ///@see <class::function> ``` //Will let you link to another function in another class
@see DialogueScript::ChangeFile()

``` ///@note <text> ``` //Will let you add a note into an area
@note This is a note

``` ///@attention <text> ``` //Will let you add a more servere note into an area
@attention This is a attention note

``` ///@warning <text> ``` //Will let you add a really servere note into an area
@warning This is a warning

``` ///~~~.cpp bool TestBool = true; ~~~ // This will allow you to show code in its normal form, doesn't work or markdown the same as it does in c# so see example on cheat sheet.

There is a lot more than what I've just shown, but this should help you get started.

@section tutorial4 How to publish changes to this site?
Once you've got some changes you would like to publish on here then:
1. open the github repo
2. switch to the master branch
3. place the files in the /docs folder, note to make sure you put the files directly in there not in the html folder. 
4. push to the repo
5. wait about a min for github pages to update
6. Done :)

