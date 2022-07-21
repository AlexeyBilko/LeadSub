using BLL.DTO;
using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubPagesService : GenericService<SubPage, SubPageDTO>
    {
        public SubPagesService(IRepository<SubPage> repository) : base(repository)
        {
        }

        public Task<IEnumerable<SubPageDTO>> GetMyAsync(string userId)
        {
            return Task.Run(() =>
            {
                return repository.GetAll()
                .Select(x =>
                    mapper.Map<SubPage, SubPageDTO>(x)
                )
                .Where(x => x.UserId == userId);
            });
        }

        /* public IEnumerable<SubPageDTO> GetAll()
         {
             return repository.GetAll()
                 .Select(x => mapper.Map<SubPage, SubPageDTO>(x));
         }

         public new Task<IEnumerable<SubPageDTO>> GetAllAsync()
         {
             return Task.Run(() =>
             {
                 return repository.GetAll()
                    .Select(x => mapper.Map<SubPage, SubPageDTO>(x));
             });
         }

         public SubPageDTO Add(SubPageDTO subpage)
         {
             SubPage newSubPage = mapper.Map<SubPageDTO, SubPage>(subpage);
             repository.Create(newSubPage);
             repository.SaveChanges();
             return subpage;
         }
         public new Task<SubPageDTO> AddAsync(SubPageDTO subpage)
         {
             return Task.Run(() => {
                 SubPage newSubPage = mapper.Map<SubPageDTO, SubPage>(subpage);
                 repository.Create(newSubPage);
                 repository.SaveChanges();
                 return subpage;
             });
         }

         public SubPageDTO Remove(SubPageDTO subpage)
         {
             SubPage subpageToRemove = repository.Get(subpage.Id);
             repository.Delete(subpageToRemove);
             repository.SaveChanges();
             return subpage;
         }
         public SubPageDTO Update(SubPageDTO subpage)
         {
             SubPage updatingSubPage = mapper.Map<SubPageDTO, SubPage>(subpage);
             repository.Update(updatingSubPage);
             repository.SaveChanges();
             return subpage;
         }
         public new Task<SubPageDTO> UpdateAsync(SubPageDTO subpage)
         {
             return Task.Run(() => {
                 SubPage updatingSubPage = repository.Get(subpage.Id);
                 updatingSubPage.InstagramLink = subpage.InstagramLink;
                 updatingSubPage.MaterialLink = subpage.MaterialLink;
                 updatingSubPage.Title = subpage.Title;
                 updatingSubPage.Avatar = subpage.Avatar;
                 updatingSubPage.Header = subpage.Header;
                 updatingSubPage.Description = subpage.Description;
                 updatingSubPage.GetButtonTitle = subpage.GetButtonTitle;
                 updatingSubPage.MainImage = subpage.MainImage;
                 updatingSubPage.SuccessDescription = subpage.SuccessDescription;
                 updatingSubPage.SuccessButtonTitle = subpage.SuccessButtonTitle;
                 repository.SaveChanges();
                 return subpage;
             });
         }
         public SubPageDTO Get(int Id)
         {
             SubPageDTO subpage = GetAll().FirstOrDefault(x => x.Id == Id);
             return subpage;

         }
         public Task<SubPageDTO> GetAsync(int Id)
         {
             return Task.Run(() =>
             {
                 SubPageDTO bicycle = GetAll().FirstOrDefault(x => x.Id == Id);
                 return bicycle;
             });
         }*/
    }
}
