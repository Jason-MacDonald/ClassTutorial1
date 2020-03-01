using System;

namespace Version_1_C
{
    [Serializable()] 
    public abstract class clsWork
    {
        private string name;
        private DateTime date = DateTime.Now;
        private decimal value;

        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }
        public decimal Value { get => value; set => this.value = value; }

        public clsWork()
        {
            EditDetails();
        }

        public abstract void EditDetails();

         public static clsWork NewWork()
         {
             InputBox lcInputBox = new InputBox("Select an artwork type from the drop down box.");

             if (lcInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             {
                 string lcReply = lcInputBox.getAnswer();

                 switch (lcReply)
                 {
                     case "Painting": return new clsPainting();
                     case "Sculpture": return new clsSculpture();
                     case "Photograph": return new clsPhotograph();
                     default: return null;
                 }
             }
             else
             {
                 lcInputBox.Close();
                 return null;
             }
         }

        public override string ToString()
        {
            return Name + "\t" + Date.ToShortDateString();  
        }
    }
}
