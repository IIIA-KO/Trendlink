import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";
import StatisticsBar from "../components/StatisticsBar";
import like from "../assets/icons/Fill.svg"
import save from "../assets/icons/save-icon.svg"
import view from "../assets/icons/views-icon.svg"
import right from "../assets/icons/navigation-chevron-right.svg"
import left from "../assets/icons/navigation-chevron-left.svg"

const EditPage: React.FC = () => {

    const { posts, loading, fetchPosts, hasNextPage, hasPreviousPage, afterCursor, beforeCursor } = useProfile();
    const navigate = useNavigate();

    if (loading) {
        navigate("/loading")
    }

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div className="w-[1047px] h-[621px] relative">
  <div className="left-[500px] top-[195px] absolute text-center text-[#616161] text-sm font-normal font-['Inter']">alesya09666@gmail.com</div>
  <div className="w-[299px] h-[54px] left-[120px] top-[253px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-[19px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Ім'я</div>
  </div>
  <div className="w-[299px] h-[121px] left-[748px] top-[252px] absolute">
    <div className="w-[299px] h-[101px] left-0 top-[20px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Про себе:</div>
  </div>
  <div className="w-[299px] h-[54px] left-[120px] top-[319px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-[19px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Прізвище</div>
  </div>
  <div className="w-[299px] h-[54px] left-[120px] top-[385px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-[19px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Дата народження</div>
  </div>
  <div className="w-[133px] h-[22px] left-[515px] top-[599px] absolute">
    <div className="w-[22px] h-[22px] left-0 top-0 absolute">
      <div className="w-[22px] h-[22px] left-0 top-0 absolute bg-[#d9d9d9]/20 rounded-full" />
      <div className="w-[15px] h-[15px] left-[3px] top-[4px] absolute" />
    </div>
    <div className="left-[27px] top-[4px] absolute text-[#616161] text-xs font-normal font-['Inter']">Видалити аккаунт</div>
  </div>
  <div className="w-[299px] h-[54px] left-[120px] top-[451px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-[19px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Номер телефону</div>
  </div>
  <div className="w-[299px] h-[35px] left-[748px] top-[404px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-0 absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="w-[86.58px] left-[30.20px] top-[10px] absolute text-[#616161] text-xs font-light font-['Inter']">Змінити пошту</div>
    <div className="w-[15.10px] h-[15px] left-[10.07px] top-[10px] absolute" />
    <div className="w-[24.16px] h-6 left-[265.78px] top-[6px] absolute" />
  </div>
  <div className="w-[299px] h-[35px] left-[748px] top-[470px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-0 absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="w-[92.62px] left-[30.20px] top-[10px] absolute text-[#616161] text-xs font-light font-['Inter']">Змінити пароль</div>
    <div className="w-[15.10px] h-[15px] left-[10.07px] top-[10px] absolute" />
    <div className="w-[24.16px] h-6 left-[265.78px] top-[6px] absolute" />
  </div>
  <div className="w-[299px] h-[54px] left-[434px] top-[451px] absolute">
    <div className="w-[299px] h-[35px] left-0 top-[19px] absolute bg-[#d9d9d9] rounded-[5px]" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Країна, місто</div>
  </div>
  <div className="left-[505px] top-[171px] absolute text-black text-base font-bold font-['Inter']">Давиденко Наталія</div>
  <div className="left-[529px] top-[136px] absolute text-[#616161] text-xs font-light font-['Inter'] underline">Завантажити фото</div>
  <img className="w-[110px] h-[110px] left-[528px] top-[21px] absolute rounded-full" src="https://via.placeholder.com/110x110" />
  <img className="w-[85px] h-[64.15px] left-0 top-0 absolute" src="https://via.placeholder.com/85x64" />
  <div className="w-[299px] h-[47px] px-2.5 py-3.5 left-[434px] top-[547px] absolute bg-[#009d9f] rounded-[39px] justify-center items-center gap-2.5 inline-flex">
    <div className="text-white text-base font-normal font-['Inter']">Зберегти</div>
  </div>
  <div className="w-[299px] h-[55px] left-[434px] top-[385px] absolute">
    <div className="w-6 h-6 left-[289px] top-[25px] absolute origin-top-left rotate-90" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Категорія</div>
    <div className="w-[299px] h-[35px] pl-2 pr-2.5 py-[5px] left-0 top-[20px] absolute bg-[#d9d9d9] rounded-[5px] justify-between items-center inline-flex">
      <div className="text-[#444444]/0 text-sm font-normal font-['Inter']">Мода і стиль</div>
      <div className="w-6 h-6 relative origin-top-left rotate-90" />
    </div>
  </div>
  <div className="w-[299px] h-[55px] left-[434px] top-[319px] absolute">
    <div className="w-6 h-6 left-[289px] top-[25px] absolute origin-top-left rotate-90" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Тип аккаунту</div>
    <div className="w-[299px] h-[35px] pl-2 pr-2.5 py-[5px] left-0 top-[20px] absolute bg-[#d9d9d9] rounded-[5px] justify-between items-center inline-flex">
      <div className="text-[#444444]/0 text-sm font-normal font-['Inter']">Мода і стиль</div>
      <div className="w-6 h-6 relative origin-top-left rotate-90" />
    </div>
  </div>
  <div className="w-[299px] h-[55px] left-[434px] top-[253px] absolute">
    <div className="w-6 h-6 left-[289px] top-[25px] absolute origin-top-left rotate-90" />
    <div className="left-[8px] top-0 absolute text-[#616161] text-xs font-light font-['Inter']">Стать</div>
    <div className="w-[299px] h-[35px] pl-2 pr-2.5 py-[5px] left-0 top-[20px] absolute bg-[#d9d9d9] rounded-[5px] justify-between items-center inline-flex">
      <div className="text-[#444444]/0 text-sm font-normal font-['Inter']">Мода і стиль</div>
      <div className="w-6 h-6 relative origin-top-left rotate-90" />
    </div>
  </div>
</div>

        </div>
    );
};

export default EditPage;