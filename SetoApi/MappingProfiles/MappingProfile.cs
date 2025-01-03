using AutoMapper;
using SetoClass.DTOs.Blog;
using SetoClass.DTOs.Game;
using SetoClass.DTOs.Resume;
using SetoClass.DTOs.ResumeSkill;
using SetoClass.DTOs.ResumeSocial;
using SetoClass.DTOs.Settings;
using SetoClass.DTOs.User;
using SetoClass.DTOs.Video;
using SetoClass.Models;

namespace SetoApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Blog Mappings
            CreateMap<Blog, BlogReadDto>();
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();

            // Diğer model mappings...

            // Örneğin:
            CreateMap<Game, GameReadDto>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>();

            // Resume Mappings
            CreateMap<Resume, ResumeReadDto>();
            CreateMap<ResumeCreateDto, Resume>();
            CreateMap<ResumeUpdateDto, Resume>();

            // ResumeSkill Mappings
            CreateMap<ResumeSkill, ResumeSkillReadDto>();
            CreateMap<ResumeSkillCreateDto, ResumeSkill>();
            CreateMap<ResumeSkillUpdateDto, ResumeSkill>();

            // ResumeSocial Mappings
            CreateMap<ResumeSocial, ResumeSocialReadDto>();
            CreateMap<ResumeSocialCreateDto, ResumeSocial>();
            CreateMap<ResumeSocialUpdateDto, ResumeSocial>();

            // Settings Mappings
            CreateMap<Settings, SettingsReadDto>();
            CreateMap<SettingsCreateDto, Settings>();
            CreateMap<SettingsUpdateDto, Settings>();

            // Video Mappings
            CreateMap<Video, VideoReadDto>();
            CreateMap<VideoCreateDto, Video>();
            CreateMap<VideoUpdateDto, Video>();


            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();


        }

    }
}
