using AutoMapper;
using Domain.Models.DAO.DailyMetrics;
using Domain.Models.DAO.Doctor;
using Domain.Models.DAO.Newses;
using Domain.Models.Entities;
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
			CreateMap<Doctor, DoctorDto>();
			CreateMap<UserReport, UserReportDto>();
			CreateMap<UserReportAddDto, UserReport>();
			CreateMap<DailyMetricAddModel, DailyMetric>();
			CreateMap<AddOrUpdateNewsDto, News>();
		}
	}
}
