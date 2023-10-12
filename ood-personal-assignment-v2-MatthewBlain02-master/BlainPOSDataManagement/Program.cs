using System;
using project;

namespace BlainPOSDataManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Establishing Connection To Database");
            BlainPOSDB db = new BlainPOSDB();
            Console.WriteLine("Creating Products And Categories");
            // Adding Fruit & Veg With Some Products
            Category c1 = new Category() { CategoryID = 1, CategoryName = "Fruit & Veg" };
            Product apple = new Product() { ProductID = 1, ProductName = "Apple", CategoryID = 1, ProductPrice = 0.60m, ProductImg = "imgs/apple.jpg", Category = c1 };
            Product banana = new Product() { ProductID = 2, ProductName = "Banana", CategoryID = 1, ProductPrice = 0.35m, ProductImg = "imgs/banana.jpg", Category = c1 };
            Product strawberry = new Product() { ProductID = 3, ProductName = "Strawberries", CategoryID = 1, ProductPrice = 3.00m, ProductImg = "imgs/strawberry.jpg", Category = c1 };
            Product orange = new Product() { ProductID = 4, ProductName = "Orange", CategoryID = 1, ProductPrice = 0.60m, ProductImg = "imgs/orange.jpg", Category = c1 };
            Product lemon = new Product() { ProductID = 5, ProductName = "Lemon", CategoryID = 1, ProductPrice = 0.40m, ProductImg = "imgs/lemon.jpg", Category = c1 };
            Product potatoes = new Product() { ProductID = 6, ProductName = "Potatoes (5kg)", CategoryID = 1, ProductPrice = 5.00m, ProductImg = "imgs/potatoes5kg.jpg", Category = c1 };
            Product cabbage = new Product() { ProductID = 7, ProductName = "Cabbage", CategoryID = 1, ProductPrice = 1.50m, ProductImg = "imgs/cabbage.jpg", Category = c1 };
            Product mushrooms = new Product() { ProductID = 8, ProductName = "mushrooms", CategoryID = 1, ProductPrice = 1.00m, ProductImg = "imgs/mushrooms.jpg", Category = c1 };
            Product eggPlant = new Product() { ProductID = 9, ProductName = "Egg Plant", CategoryID = 1, ProductPrice = 1.00m, ProductImg = "imgs/eggplant.jpg", Category = c1 };
            //Adding Ambiant (Shelf Items) With Some Products
            Category c2 = new Category() { CategoryID = 2, CategoryName = "Ambiant" };
            Product bakedBeans = new Product() { ProductID = 10, ProductName = "Baked Beans", CategoryID = 2, ProductPrice = 0.80m, ProductImg = "imgs/bakedbeans.jpg", Category = c2 };
            Product richTeaBiscuit = new Product() { ProductID = 11, ProductName = "Rich Tea Biscuit", CategoryID = 2, ProductPrice = 1.00m, ProductImg = "imgs/richteabiscuits.jpg", Category = c2 };
            Product tea = new Product() { ProductID = 12, ProductName = "Tea Bags", CategoryID = 2, ProductPrice = 2.50m, ProductImg = "imgs/teabags.jpg", Category = c2 };
            Product coffee = new Product() { ProductID = 13, ProductName = "Coffee", CategoryID = 2, ProductPrice = 3.50m, ProductImg = "imgs/coffee.jpg", Category = c2 };
            Product crisps = new Product() { ProductID = 14, ProductName = "Tayto C + O", CategoryID = 2, ProductPrice = 0.80m, ProductImg = "imgs/taytosco.jpg", Category = c2 };
            Product chocolate = new Product() { ProductID = 15, ProductName = "Chocolate Bar", CategoryID = 2, ProductPrice = 1.20m, ProductImg = "imgs/chocolatebar.png", Category = c2 };
            //Adding Frozen With Some Products
            Category c3 = new Category() { CategoryID = 3, CategoryName = "Frozen" };
            Product pizza = new Product() { ProductID = 16, ProductName = "Pizza (Pepperoni)", CategoryID = 3, ProductPrice = 4.25m, ProductImg = "imgs/pepperonipizza.jpg", Category = c3 };
            Product chips = new Product() { ProductID = 17, ProductName = "Crinkle Cut Chips", CategoryID = 3, ProductPrice = 3.25m, ProductImg = "imgs/crinklecutchips.jpg", Category = c3 };
            Product iceCream = new Product() { ProductID = 18, ProductName = "Magnums 3 PK (White Choc)", CategoryID = 3, ProductPrice = 4.25m, ProductImg = "imgs/whitechocmagnum3pk.jpg", Category = c3 };
            Product frozenPeas = new Product() { ProductID = 19, ProductName = "Frozen Peas", CategoryID = 3, ProductPrice = 2.45m, ProductImg = "imgs/frozenpeas.jpg", Category = c3 };
            Product ice = new Product() { ProductID = 20, ProductName = "Ice (1kg)", CategoryID = 3, ProductPrice = 1.25m, ProductImg = "imgs/ice.jpg", Category = c3 };
            //Adding Off Licence With Products
            Category c4 = new Category() { CategoryName = "Off Licence" };
            Product smirnoffvodkam = new Product() { ProductID = 21, ProductName = "Smirnoff Vodka (70cl)", CategoryID = 4, ProductPrice = 21.99m, ProductImg = "imgs/smirnoffvodka70cl.jpg", Category = c4 };
            Product smirnoffvodkanaggin = new Product() { ProductID = 22, ProductName = "Smirnoff Vodka (20cl)", CategoryID = 4, ProductPrice = 12.45m, ProductImg = "imgs/smirnoffvodkanaggin.jpg", Category = c4 };
            Product heineken6pack = new Product() { ProductID = 23, ProductName = "Heineken 6 PK Cans", CategoryID = 4, ProductPrice = 12.00m, ProductImg = "imgs/heineken6pkcans.jpg", Category = c4 };
            Product coors6pack = new Product() { ProductID = 24, ProductName = "Coors 6 PK Cans", CategoryID = 4, ProductPrice = 12.00m, ProductImg = "imgs/coors6pkcans.jpg", Category = c4 };
            Product yellowtailpg = new Product() { ProductID = 25, ProductName = "Yellow Tail PG", CategoryID = 4, ProductPrice = 9.50m, ProductImg = "imgs/yellowtailpg.jpg", Category = c4 };
            Product yellowtailcs = new Product() { ProductID = 26, ProductName = "Yellow Tail CS", CategoryID = 4, ProductPrice = 9.50m, ProductImg = "imgs/yellowtailcs.jpg", Category = c4 };
            //Adding Bakery With Products
            Category c5 = new Category() { CategoryName = "Bakery" };
            Product appleTart = new Product() { ProductID = 27, ProductName = "Apple Tart", CategoryID = 5, ProductPrice = 4.50m, ProductImg = "imgs/appletart.jpg", Category = c5 };
            Product scone = new Product() { ProductID = 28, ProductName = "Scone", CategoryID = 5, ProductPrice = 0.90m, ProductImg = "imgs/scone.jpg", Category = c5 };
            Product cakeChoc = new Product() { ProductID = 29, ProductName = "Chocolate Cake", CategoryID = 5, ProductPrice = 10.50m, ProductImg = "imgs/chocolatecake.jpg", Category = c5 };
            //Adding Categories To DB
            Console.WriteLine("Adding Categories To DB");
            db.Categories.Add(c1);
            db.Categories.Add(c2);
            db.Categories.Add(c3);
            db.Categories.Add(c4);
            db.Categories.Add(c5);
            //Adding Products To DB
            Console.WriteLine("Adding Products To DB");
            db.Products.Add(apple);
            db.Products.Add(banana);
            db.Products.Add(strawberry);
            db.Products.Add(orange);
            db.Products.Add(lemon);
            db.Products.Add(potatoes);
            db.Products.Add(cabbage);
            db.Products.Add(mushrooms);
            db.Products.Add(eggPlant);
            db.Products.Add(bakedBeans);
            db.Products.Add(richTeaBiscuit);
            db.Products.Add(tea);
            db.Products.Add(coffee);
            db.Products.Add(crisps);
            db.Products.Add(chocolate);
            db.Products.Add(pizza);
            db.Products.Add(chips);
            db.Products.Add(iceCream);
            db.Products.Add(frozenPeas);
            db.Products.Add(ice);
            db.Products.Add(smirnoffvodkam);
            db.Products.Add(smirnoffvodkanaggin);
            db.Products.Add(heineken6pack);
            db.Products.Add(coors6pack);
            db.Products.Add(yellowtailpg);
            db.Products.Add(yellowtailcs);
            db.Products.Add(appleTart);
            db.Products.Add(scone);
            db.Products.Add(cakeChoc);
            
            // Adding TransactionTypes To DB
            TransactionType Cash = new TransactionType { TransactionTypeID = 1, TransactionTypeName = "Cash", TransactionTypeImg = "imgs/cash.jpg" };
            TransactionType Card = new TransactionType { TransactionTypeID = 2, TransactionTypeName = "Card", TransactionTypeImg = "imgs/card.png" };
            Console.WriteLine("Adding Transaction Types");
            db.TransactionTypes.Add(Cash);
            db.TransactionTypes.Add(Card);
            //Saving Changes To DB
            db.SaveChanges();

            Console.WriteLine("Saved Changes To DB");
            Console.Write("Press Enter To Continue");
            Console.ReadLine();
            
            


        }
    }
}
