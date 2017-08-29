using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trainee.Business.DAL.Entities;

namespace TestWeb.Models.HomeViewModels
{
	public class IndexViewModel
	{
        public List<FrontPageItem> FrontPageItems { get; set; }		
    }
}
