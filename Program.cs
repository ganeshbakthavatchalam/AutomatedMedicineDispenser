using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedMedicineCabinet
{
    public class Program
    {
        static void Main(string[] args)
        {
            MedicineDispenser objMedicineDispenser = new MedicineDispenser();

            //Large Bin Add unit test cases
            RequestObject objRequestLB1 = new RequestObject();
            objRequestLB1.TypeofBin = BinType.Large;
            objRequestLB1.BinNumber = 1;
            objRequestLB1.MedicineId = 1001;
            objRequestLB1.MedicineName = "LZofran";

            RequestObject objRequestLB2 = new RequestObject();
            objRequestLB2.TypeofBin = BinType.Large;
            objRequestLB2.BinNumber = 2;
            objRequestLB2.MedicineId = 1002;
            objRequestLB2.MedicineName = "LFolic";

            for (int nCount = 0; nCount <= 15; nCount++)
            {
                Console.WriteLine(objMedicineDispenser.Add(objRequestLB1));
                Console.WriteLine(objMedicineDispenser.Add(objRequestLB2));
            }

            //Medium Bin Add unit test cases
            RequestObject objRequestMB1 = new RequestObject();
            objRequestMB1.TypeofBin = BinType.Medium;
            objRequestMB1.BinNumber = 1;
            objRequestMB1.MedicineId = 1001;
            objRequestMB1.MedicineName = "LZofran";

            RequestObject objRequestMB2 = new RequestObject();
            objRequestMB2.TypeofBin = BinType.Medium;
            objRequestMB2.BinNumber = 2;
            objRequestMB2.MedicineId = 1002;
            objRequestMB2.MedicineName = "LFolic";

            RequestObject objRequestMB3 = new RequestObject();
            objRequestMB3.TypeofBin = BinType.Medium;
            objRequestMB3.BinNumber = 3;
            objRequestMB3.MedicineId = 1002;
            objRequestMB3.MedicineName = "LFolic";

            for (int nCount = 0; nCount <= 10; nCount++)
            {
                Console.WriteLine(objMedicineDispenser.Add(objRequestMB1));
                Console.WriteLine(objMedicineDispenser.Add(objRequestMB2));
                Console.WriteLine(objMedicineDispenser.Add(objRequestMB3));
            }

            //Small Bin Add unit test cases
            RequestObject objRequestSB1 = new RequestObject();
            objRequestSB1.TypeofBin = BinType.Small;
            objRequestSB1.BinNumber = 1;
            objRequestSB1.MedicineId = 1001;
            objRequestSB1.MedicineName = "LZofran";

            RequestObject objRequestSB2 = new RequestObject();
            objRequestSB2.TypeofBin = BinType.Small;
            objRequestSB2.BinNumber = 2;
            objRequestSB2.MedicineId = 1002;
            objRequestSB2.MedicineName = "LFolic";

            for (int nCount = 0; nCount <= 5; nCount++)
            {
                Console.WriteLine(objMedicineDispenser.Add(objRequestSB1));
                Console.WriteLine(objMedicineDispenser.Add(objRequestSB2));
            }

            //Remove units from Bin
            Console.WriteLine(objMedicineDispenser.Remove(objRequestLB1, 13));



            Console.ReadLine();
        }
    }

    public class RequestObject
    {
        public BinType TypeofBin { get; set; }
        public int BinNumber { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }

    public enum BinType
    {
        Large = 0,
        Medium = 1,
        Small = 2
    }
    public class Cabinet
    {
        public BinType TypeofBin { get; set; }
        public List<Bin> Bins { get; set; }
    }
    public class Bin
    {
        public int BinNumber { get; set; }
        public List<Unit> Units { get; set; }
    }
    public class Unit
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }

    public class MedicineDispenser
    {
        List<Cabinet> lstCabinets = new List<Cabinet>();

        List<int> lstLargeBinNumbers = new List<int>(2) { 1, 2 };
        List<int> lstMediumBinNumbers = new List<int>(5) { 1, 2, 3, 4, 5 };
        List<int> lstSmallBinNumbers = new List<int>(3) { 1, 2, 3 };
        int unitsCount = 0;

        public string Add(RequestObject objRequest)
        {
            string result = string.Empty;
            switch (objRequest.TypeofBin)
            {
                case BinType.Large:
                    if (lstLargeBinNumbers.Contains(objRequest.BinNumber))
                    {
                        unitsCount = 0;
                        result = AddUnitsToBin(objRequest, 15);
                    }
                    break;

                case BinType.Medium:
                    if (lstMediumBinNumbers.Contains(objRequest.BinNumber))
                    {
                        unitsCount = 0;
                        result = AddUnitsToBin(objRequest, 10);
                    }
                    break;

                case BinType.Small:
                    if (lstSmallBinNumbers.Contains(objRequest.BinNumber))
                    {
                        unitsCount = 0;
                        result = AddUnitsToBin(objRequest, 5);
                    }
                    break;
            }
            return result;
        }

        private string AddUnitsToBin(RequestObject objRequest, int maxUnits)
        {
                if (lstCabinets?.Any (x => x.TypeofBin == objRequest.TypeofBin && x.Bins.Any(y => y.BinNumber == objRequest.BinNumber)) ?? false)
                {
                    var bintypes = lstCabinets.FindAll(x => x.TypeofBin == objRequest.TypeofBin);
                    
                    bintypes.ForEach(bintype =>
                    {
                        var bins = bintype.Bins.Find(x => x.BinNumber == objRequest.BinNumber);
                        if(bins != null)
                        {
                            unitsCount = bins.Units.Count();
                        }

                    });

                }

                else
                {
                    List<Bin> lstBin = new List<Bin>();

                    Unit objUnit = new Unit();
                    objUnit.MedicineId = objRequest.MedicineId;
                    objUnit.MedicineName = objRequest.MedicineName;

                    Bin objBin = new Bin();
                    objBin.BinNumber = objRequest.BinNumber;
                    objBin.Units = new List<Unit>();
                    objBin.Units.Add(objUnit);

                    lstBin.Add(objBin);

                    lstCabinets.Add(new Cabinet { TypeofBin = objRequest.TypeofBin, Bins = lstBin });
                }

                if (unitsCount > 0 && unitsCount < maxUnits)
                {
                    Bin loadBin = null;
                    var bintypes = lstCabinets.FindAll(x => x.TypeofBin == objRequest.TypeofBin);

                    bintypes.ForEach(bintype =>
                    {
                        var bins = bintype.Bins.Find(x => x.BinNumber == objRequest.BinNumber);
                        if (bins != null)
                        {
                            loadBin = bins;
                        }

                    });

                    if (loadBin != null)
                    {
                        Unit objUnit = new Unit();
                        objUnit.MedicineId = objRequest.MedicineId;
                        objUnit.MedicineName = objRequest.MedicineName;

                        loadBin.Units.Add(objUnit);
                    }
                }
                else if(unitsCount >= maxUnits)
                {
                    return string.Format("{0} Bin {1}  is full", objRequest.TypeofBin, objRequest.BinNumber);
                }
            
            return string.Format("Unit Added to {0} Bin {1} Successfully", objRequest.TypeofBin, objRequest.BinNumber);
        }

        public string Remove(RequestObject objRequest, int unitsToRemove)
        {
            string result = string.Empty;
            switch (objRequest.TypeofBin)
            {
                case BinType.Large:
                    if (lstLargeBinNumbers.Contains(objRequest.BinNumber))
                    {
                        result = RemoveUnitFromBin(objRequest, unitsToRemove, 15);
                    }
                    break;

                case BinType.Medium:
                    if (lstMediumBinNumbers.Contains(objRequest.BinNumber))
                    {
                        result = RemoveUnitFromBin(objRequest, unitsToRemove, 10);
                    }
                    break;

                case BinType.Small:
                    if (lstSmallBinNumbers.Contains(objRequest.BinNumber))
                    {
                        result = RemoveUnitFromBin(objRequest, unitsToRemove, 5);
                    }
                    break;
            }
            return result;
        }

        private string RemoveUnitFromBin(RequestObject objRequest, int unitsToRemove, int maximumQuantity)
        {
            string result = string.Empty;

            if (lstCabinets?.Any(x => x.TypeofBin == objRequest.TypeofBin && x.Bins.Any(y => y.BinNumber == objRequest.BinNumber)) ?? false)
            {
                var bintypes = lstCabinets.FindAll(x => x.TypeofBin == objRequest.TypeofBin);

                bintypes.ForEach(bintype =>
                {
                    var bins = bintype.Bins.Find(x => x.BinNumber == objRequest.BinNumber);
                    if (bins != null)
                    {
                        for (int nCount = 0; nCount < unitsToRemove; nCount++) {
                            if (bins.Units?.Any() ?? false)
                            {
                                bins.Units.RemoveAt(unitsToRemove - nCount);
                            }
                        }

                        if (((bins.Units.Count() * 100 / maximumQuantity)) < 20)
                        {
                          result =  string.Format("{0} Bin is less than 20% quanity. ", objRequest.TypeofBin);
                        }

                    }

                });

                result += "Removed Unit successfully";
            }

            return result;
        }
    }
}
