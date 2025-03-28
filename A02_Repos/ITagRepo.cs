using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;

namespace A02_Repos
{
    public interface ITagRepo
    {
        public Tag GetTagsId(int id);

        public List<Tag> GetAllTag();

        public void Add(Tag tag);

        public void Update(Tag tag);

        public void Delete(int id);
    }
}