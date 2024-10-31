using AutoMapper;
using Domain.Models.DAO.DailyMetrics;
using Domain.Models.Entities.YourNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<DailyMetric, DailyMetricReturnModel>();
		}
	}
}
