using System.Threading.Tasks;
using AutoMapper;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Models.Members;
using Microsoft.EntityFrameworkCore;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberService
    {
        Task<MemberModel> FindByIdAsync(int id);
        Task<MemberModel> FindByEmailAsync(string email);
        Task<MemberModel> FindBySubjectIdAsync(string subjectId);
    }

    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IRepository<Member> memberRepository,
            IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberModel>  FindByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }

        public async Task<MemberModel> FindByEmailAsync(string email)
        {
            var member = await _memberRepository.TableNoTracking.FirstOrDefaultAsync(x => x.EmailAddress == email);
            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }

        public async Task<MemberModel> FindBySubjectIdAsync(string subjectId)
        {
            var member = await _memberRepository.TableNoTracking.FirstOrDefaultAsync(x => x.SubjectId == subjectId);
            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }
    }
}
