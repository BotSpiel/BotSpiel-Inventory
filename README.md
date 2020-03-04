# BotSpiel-Inventory

This repository contains an example project that was generated using BotSpiel. It aims to illustrate the style of software infrastructure that is generated by [BotSpiel](https://www.botspiel.com/). It also shows how the generated software solution can then be changed to create a fully fledged web application and/or bot. You can get a copy of [BotSpiel](https://www.botspiel.com/) via our website or directly from [Dominika](https://dominika.botspiel.com/) our shop bot.

This example is a basic inventory/warehouse web app/bot. It may seem an unusual choice for a bot, but this example illustrates that "one box" apps can easily be extended to channels such as SMS/Text and other chat channels. All the mobile interactions such as putting away and picking inventory are transacted via the bot (embedded in the web app). It also shows how a traditional web app and a bot can be jointly deployed to service the different user interaction contexts. This, coupled with the ability to process natural language means the user is "hands-free".

The application is fully functional, but it is NOT PRODUCTION READY software. It is only used to demonstrate what can be done with BotSpiel.

## Generated code vs manually added code

All the code in the Visual Studio solution and included database was generated by BotSpiel, loaded with an inventory domain model. 

The generated infrastructure code was then changed to implement the desired behavior. All added, changed or deleted code is marked up using the following convention:

###### Changed code
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
                ... the original generated code
            //Replaced Code Block End
                ... the changed code
            //Custom Code End
###### Added code
            //Custom Code Start | Added Code Block 
                ... the code added			
            //Custom Code End
###### Deleted code
            //Custom Code Start | Removed Block 
                ... the commented code				
            //Custom Code End	

The code changes can be easily found by searching for "//Custom Code".

## Downloading and running the web/bot app

If you are interested in downloading and running the example - here are the basic steps:
1. Unzip the download
2. Restore the database from the **Database** folder. You will need SQL Server 14.0.1000 and up. The latest SQL Server Express Edition will suffice.
3. Maintain the DefaultConnection in the appsettings.json file
4. Publish the project or run in Debug mode from Visual Studio (2017 or later)


