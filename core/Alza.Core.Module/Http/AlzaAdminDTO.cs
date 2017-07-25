using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Core.Module.Http
{
    public class AlzaAdminDTO
    {
        private AlzaAdminDTO(Object data)
        {
            if (data != null)
            {
                this.isOK = true;
                this.isEmpty = false;
                this.data = data;
            }
            else
            {
                this.isOK = true;
                this.isEmpty = true;
            }
        }
        private AlzaAdminDTO(bool isOk)
        {
            this.isOK = isOk;
            this.isEmpty = true;
        }
        private AlzaAdminDTO(bool isOk, Guid errorNo, string errorText)
        {
            this.isOK = isOk;
            this.isEmpty = true;
            this.errorNo = errorNo;
            this.errors.Add(errorText);
        }




        public bool isOK { get; set; }
        public bool isEmpty { get; set; }
        public List<string> errors { get; set; } = new List<string>();
        public Object data { get; set; }

        public Guid errorNo { get; set; }
        public string errorText {
            get
            {
                StringBuilder res = new StringBuilder();

                foreach (var item in errors)
                {
                    res.AppendLine(item);
                }

                return res.ToString();
            }
            private set { }
        }



        public static AlzaAdminDTO False
        {
            get
            {
                return new AlzaAdminDTO(false);
            }
        }

        public static AlzaAdminDTO True
        {
            get
            {
                return new AlzaAdminDTO(true);
            }
        }

        public static AlzaAdminDTO Empty
        {
            get
            {
                return new AlzaAdminDTO(true);
            }
        }


        
        public static AlzaAdminDTO Error(Guid errorNo, string errorText)
        {
            return new AlzaAdminDTO(false, errorNo, errorText);
        }

        public static AlzaAdminDTO Data(Object data)
        {
            return new AlzaAdminDTO(data);
        }

        public static AlzaAdminDTO Error(string message)
        {
            Guid guid = new Guid();
            return AlzaAdminDTO.Error(guid, message);
        }
    }
}
