using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperConfiguration
{
    public static class AutoMapperConfigurator
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
