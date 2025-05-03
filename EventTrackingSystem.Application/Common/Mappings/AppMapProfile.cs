using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Mappings;

public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        CreateMap<UserEntity, UserDto>().ReverseMap();
        CreateMap<SignUpDto, UserEntity>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<CreateEventDto, EventEntity>()
            .ForMember(dest => dest.PreviewPhoto, opt => opt.Ignore());

        CreateMap<EventDto, EventEntity>();

        CreateMap<EventEntity, EventDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleEvents.Select(re => re.Role)));

        CreateMap<RoleDto, RoleEntity>().ReverseMap();
        CreateMap<MediaDto, MediaEntity>().ReverseMap();

        CreateMap<SpecialtyEntity, SpecialtyDto>();

        CreateMap<CreateSpecialtyDto, SpecialtyEntity>()
            .ForMember(dest => dest.Photo, opt => opt.Ignore());

        CreateMap<UpdateSpecialtyDto, SpecialtyEntity>()
            .ForMember(dest => dest.Photo, opt => opt.Ignore());

        CreateMap<PartnerEntity, PartnerDto>();

        CreateMap<AchievementEntity, AchievementDto>();

        CreateMap<AchievementCreateDto, AchievementEntity>()
               .ForMember(dest => dest.PhotoPath, opt => opt.Ignore());

        CreateMap<AchievementUpdateDto, AchievementEntity>()
            .ForMember(dest => dest.PhotoPath, opt => opt.Ignore());


        CreateMap<CreateGalleryDto, GalleryEntity>()
               .ForMember(dest => dest.PreviewPhotoPath, opt => opt.Ignore());

        CreateMap<UpdateGalleryDto, GalleryEntity>()
           .ForMember(dest => dest.PreviewPhotoPath, opt => opt.Ignore());

        CreateMap<GalleryEntity, GalleryDto>()
            .ForMember(dest => dest.PreviewPhoto, opt => opt.MapFrom(src => src.PreviewPhotoPath))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(photo => new GalleryPhotoDto { PhotoPath = photo.PhotoPath, Id = photo.Id })));

        CreateMap<Telegram.Bot.Types.Chat, TelegramChatEntity>()
            .ForMember(x => x.ChatId, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Id, opt => opt.Ignore());

    }
}
