﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vega.Models;
using Vega.Models.ApiResources;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                    {
                        Email = v.ContactEmail,
                        Name = v.ContactName,
                        Phone = v.ContactPhone
                    }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource{Id = vf.Feature.Id, Name = vf.Feature.Name})));

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                    {
                        Email = v.ContactEmail,
                        Name = v.ContactName,
                        Phone = v.ContactPhone
                    }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(f => f.FeatureId)));



            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
	            {
                    //Remove some
		            v.Features
                        .Where(f => !vr.Features.Contains(f.FeatureId))
                        .ToList()
                        .ForEach(vf => v.Features.Remove(vf));

                    //Add some
		            vr.Features
                        .Where(id => !v.Features.Select(f => f.FeatureId).Contains(id))
                        .ToList()
                        .ForEach(fid => v.Features.Add(new VehicleFeature {FeatureId = fid, VehicleId = v.Id}));
	            });

        }
    }
}
