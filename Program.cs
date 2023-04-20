using Console_Library_Management_Tool;

var user = new Customer("Om Khadka", "WhatsGoodieMyFellowGangMembers69420!");
var library = new Library("My Library", BookGenerator.GenerateBooks(100));
var menu = new Menu(library, user);

menu.MainMenu();