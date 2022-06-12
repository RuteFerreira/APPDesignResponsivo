namespace APPDB.Models {
    public class ItemCombo {
        public int Id { get; set; } 
        public string Designacao { get; set; }

        public ItemCombo() {
            Id = 0;
            Designacao = "";
        }
    }
}
