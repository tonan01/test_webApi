using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Controllers.Models;
namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        #region API
        //Lấy (hiển thị)
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);//Thành công
        }

        //Lấy (hiển thị) có id
        [HttpGet("{pId}")]
        public IActionResult GetById(string pId)
        {
            try
            {
                var hangHoa = hangHoas.FirstOrDefault(p => p.MaHangHoa == Guid.Parse(pId));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                return Ok(hangHoa);
            }
            catch
            {
                return BadRequest();
            }
        }

        //Thêm mới
        [HttpPost]
        public IActionResult Create(HangHoaVM pHangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = pHangHoaVM.TenHangHoa,
                DonGia = pHangHoaVM.DonGia
            };
            hangHoas.Add(hangHoa);
            return Ok(new
            {
                Success = true,
                Data = hangHoa
            });
        }

        //Cập nhật
        [HttpPut("{pId}")]
        public IActionResult Edit(string pId,HangHoa pHangHoa)
        {
            try
            {
                var hangHoa = hangHoas.FirstOrDefault(p => p.MaHangHoa == Guid.Parse(pId));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                //Update
                if(pId!= hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                hangHoa.TenHangHoa = pHangHoa.TenHangHoa;
                hangHoa.DonGia= pHangHoa.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //Xóa
        [HttpDelete("{pId}")]
        public IActionResult Remote(string pId, HangHoa pHangHoa)
        {
            try
            {
                var hangHoa = hangHoas.FirstOrDefault(p => p.MaHangHoa == Guid.Parse(pId));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                hangHoas.Remove(hangHoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
