# MusicSocial Development Notes

## General Notes

### Style

I'll detail some of my style preferences in order to avoid confusion and keep things consistent.

In a class with methods, properties, and attributes/members, I tend to keep properties and attributes at the bottom of the class definition because when determining how to implement a class, the methods are more important than variables. I also prefix private members with underscores. You'll notice both of these in the controller files, but you won't see them be relevant elsewhere given the work we're doing.

I would recommend that we stick with RESTful routing conventions for route names to avoid convusion and disorganization when different people are working on views and controllers. I haven't built out the controllers, but I did get the files set up with all the basic routes with the naming convention I was taught. We don't have to stick with those names necessarily, but if we want to go with different conventions, we just need to make sure we're all on the same page about them.

It also would probably be easiest to stick with ASP.NET's default layout, but we don't have to.

### Getting This Set Up on Your Local Machine

I still need to learn more about Git, and I do think we need to be working with branches. Working on a small MVC project in a team is already proving kind of difficult, and if we don't use branches, we'll end up having to be in constant communication before working on anything to avoid upstream conflicts on pushes. We'll have to talk tomorrow (4/26) to better delineate tasks and see how we want to configure branches.

As for when you first clone this project to your local machine, note that you will may need to create an appsettings.json file with whatever code you've been using in your appsetting.json file. I added it to the .gitignore file, so it shouldn't sync with GitHub. I also included the compiled code folders bin and obj in the .gitignore.

VS Code will likely automatically run a `dotnet restore` after cloning, but you may need to do this manually. As for Entity Framework/MySQL, I ran the initial migration, so you should only need to run `dotnet ef database update` to update your MySQL with the migration from the project.

### Commits

It would probably be good to keep track of commits, ideally with notes on changes.

4/26 "First commit" (Branch: Main)

- Updated boilerplate startup code
- Set up file structure
- Finished models (may need adjustment)
- Created controller files and added method declarations--but not implementations typically--for the basic routes to the files
- Ran initial `dotnet ef migrations add`
