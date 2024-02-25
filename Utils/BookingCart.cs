using Final.Models;
using Newtonsoft.Json;
namespace Final.Utils
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Language { get; set; }
        
        public CartItem(int id, string name, double price, string language){
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Language = language;
        }
    }
     public class BookingCart {
        public List<CartItem> Items { get; private set; }
        public BookingCart() { 

            this.Items = new List<CartItem>();
                
        }public CartItem AddItem(int productId,string title,double price, string language) {
            CartItem newItem=new CartItem(productId,title,price,language) ;
            bool itemExist=false;
            for (int i=0;i<this.Items.Count;i++) {
                if (this.Items[i].Id==productId) {
                    this.Items[i].Language+=language;
                    itemExist=true;
                    break;
                }
            }  
            // Create a new item to add to the cart
            if(!itemExist)        
            {
                this.Items.Add(newItem);
                
            }
            return newItem;
        }
        public void EditItem(int productId) {            
            foreach (CartItem item in this.Items) {
                if (item.Id==productId) {
                    // if(quantity==0)
                        this.Items.Remove(item);
                    // else
                    //     item.Language=Language;                        
                    break;
                }
            }
        }
        public void RemoveItem(int productId) {            
            this.EditItem(productId);
        }

     }
     

}