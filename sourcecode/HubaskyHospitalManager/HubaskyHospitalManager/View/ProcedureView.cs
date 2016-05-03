//using HubaskyHospitalManager.Model.PatientManagement;
//using HubaskyHospitalManager.Model.InventoryManagement;
//using HubaskyHospitalManager.Model.Common;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;

//namespace HubaskyHospitalManager.View
//{
//    public class ProcedureView : INotifyPropertyChanged
//    {
//        private ObservableCollection<InventoryItem> inventoryUsage;

//        public virtual ObservableCollection<InventoryItem> InventoryUsage
//        {
//            get { return inventoryUsage; }
//            set { inventoryUsage = value; OnPropertyChanged(); }
//        }

//        private InventoryItem selectedInventoryItem;

//        public InventoryItem SelectedInventoryItem
//        {
//            get { return selectedInventoryItem; }
//            set { selectedInventoryItem = value; OnPropertyChanged(); }
//        }



//        public virtual ObservableCollection<String> Attachments { get; set; } //nem kell view?
//        public string SelectedAttachment { get; set; }

//        //modell eredeti procedure
//        private Procedure modelProcedure;
//        public Procedure ModelProcedure
//        {
//            get { return modelProcedure; }
//            set { modelProcedure = value; OnPropertyChanged(); }
//        }

//        public ProcedureView(Procedure proc)
//        {
//            ModelProcedure = proc;
//            InventoryUsage = new ObservableCollection<InventoryItem>(ModelProcedure.InventoryUsage);
//            Attachments = new ObservableCollection<string>(ModelProcedure.Attachments);
            
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        void OnPropertyChanged([CallerMemberName] string name = "")
//        {
//            PropertyChangedEventHandler handler = PropertyChanged;
//            if (handler != null) handler(this, new PropertyChangedEventArgs(name));

//        }
//    }
//}
