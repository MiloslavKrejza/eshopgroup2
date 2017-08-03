using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Core.Module.Http
{
    public class AlzaAdminDTO<TObj>
    {
        private AlzaAdminDTO(TObj data)
        {
            if (!data.Equals(default(TObj)))
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
        public TObj data { get; set; }

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



        public static AlzaAdminDTO<TObj> False
        {
            get
            {
                return new AlzaAdminDTO<TObj>(false);
            }
        }

        public static AlzaAdminDTO<TObj> True
        {
            get
            {
                return new AlzaAdminDTO<TObj>(true);
            }
        }

        public static AlzaAdminDTO<TObj> Empty
        {
            get
            {
                return new AlzaAdminDTO<TObj>(true);
            }
        }


        
        public static AlzaAdminDTO<TObj> Error(Guid errorNo, string errorText)
        {
            return new AlzaAdminDTO<TObj>(false, errorNo, errorText);
        }

        public static AlzaAdminDTO<TObj> Data(TObj data)
        {
            return new AlzaAdminDTO<TObj>(data);
        }

        public static AlzaAdminDTO<TObj> Error(string message)
        {
            Guid guid = new Guid();
            return Error(guid, message);
        }
    }
}
