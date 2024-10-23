import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";


const  CalendarPage: React.FC = () => {

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar />
            </div>
            <div className="w-5/6 h-auto">
                <div className="flex flex-col gap-2  bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar />
                   
                    <div className="h-1/4 w-full text-center text-black">
                    <div className="h-1/4 w-full inline-block text-center text-black">
                        <div className="h-full relative  w-1/2 mx-10 my-6">
                        <div className="h-[35px] justify-start items-start gap-[5px] inline-flex">
  <div className="h-[35px] px-2.5 py-1 rounded-[5px] border border-[#c0bebe] flex-col justify-center items-start gap-2.5 inline-flex">
    <select className="w-[238px] h-4 text-[#c0bebe] text-[11px] font-normal font-['Inter']">
      <option value="" disabled selected>Введіть ключове слово</option>
      <option value="keyword1">Keyword 1</option>
      <option value="keyword2">Keyword 2</option>
  
    </select>
  </div>
  <div className="w-[178px] h-[35px] px-2.5 py-1 rounded-[5px] border border-[#c0bebe] flex-col justify-center items-start gap-2.5 inline-flex">
    <select className="w-[158px] h-4 text-[#c0bebe] text-[11px] font-normal font-['Inter']">
      <option value="" disabled selected>Оберіть період</option>
      <option value="period1">Period 1</option>
      <option value="period2">Period 2</option>

    </select>
  </div>
  <div className="h-[35px] px-[15px] py-1 rounded-[5px] border border-[#c0bebe] flex-col justify-center items-start gap-2.5 inline-flex">
    <select className="w-14 h-4 text-[#c0bebe] text-[11px] font-normal font-['Inter']">
      <option value="" disabled selected>Статус</option>
      <option value="status1">Status 1</option>
      <option value="status2">Status 2</option>
   
    </select>
  </div>
  <div className="h-[35px] px-[15px] py-1 rounded-[5px] border border-[#c0bebe] flex-col justify-center items-start gap-2.5 inline-flex">
    <select className="w-[67px] h-4 text-[#c0bebe] text-[11px] font-normal font-['Inter']">
      <option value="" disabled selected>Категорії</option>
      <option value="category1">Category 1</option>
      <option value="category2">Category 2</option>

    </select>
  </div>
  <div className="h-[35px] px-[15px] py-1 rounded-[5px] border border-[#c0bebe] flex-col justify-center items-start gap-2.5 inline-flex">
    <select className="w-[69px] h-4 text-[#c0bebe] text-[11px] font-normal font-['Inter']">
      <option value="" disabled selected>Перегляд</option>
      <option value="view1">View 1</option>
      <option value="view2">View 2</option>
      
    </select>
  </div>
</div>

                          </div>
                        </div>
                        <div className="h-full relative w-1/2 mx-10 my-6">
                        <div className="w-[930px] h-[698px] flex-col justify-start items-start gap-[46px] inline-flex">
  <div className="h-[321px] relative">
    <div className="w-[930px] h-[321px] left-0 top-0 absolute">
      <div className="w-[597px] h-[321px] p-5 left-[333px] top-0 absolute bg-[#eff7ff] rounded-[20px] flex-col justify-start items-start gap-[38px] inline-flex">
        <div className="w-4 h-4 relative">
          <img className="w-3.5 h-[13.50px] left-[1px] top-[1px] absolute" src="https://via.placeholder.com/14x13" />
        </div>
        <div className="w-[422px] h-[82px] relative">
          <div className="left-[1px] top-[1px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">Рекламна інтеграція</div>
          <div className="left-[182px] top-0 absolute text-[#3c3c3c] text-base font-normal font-['Inter']">WOW NAME -інтернет магазин</div>
          <div className="left-[1px] top-[33px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Дата і час:</div>
          <div className="left-[207px] top-[31px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">9 березня 2024</div>
          <div className="left-[357px] top-[31px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">18.00</div>
          <div className="left-0 top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Відповідальні особи:</div>
          <div className="w-5 h-5 left-[182px] top-[30px] absolute">
            <div className="w-[17.51px] h-[17.50px] left-[1.25px] top-[1.25px] absolute">
            </div>
          </div>
          <div className="w-5 h-5 left-[332px] top-[30px] absolute">
            <div className="w-[15.62px] h-[17.50px] left-[2.50px] top-[1.25px] absolute">
            </div>
          </div>
        </div>
        <div className="w-[485px] h-[81px] relative">
          <div className="left-0 top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Статус:</div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Планується</div>
          <div className="left-[1px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Посилання і документи:</div>
          <div className="left-[180px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter'] underline">Презентація</div>
          <div className="left-[1px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Коментарі і нотатки:</div>
          <div className="left-[180px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Знижка при замовленні за промокодом Pink</div>
        </div>
      </div>
      <div className="w-[597px] h-[321px] p-5 left-[333px] top-0 absolute bg-[#eff7ff] rounded-[20px] flex-col justify-start items-start gap-[30px] inline-flex">
        <div className="w-4 h-4 relative">
          <img className="w-3.5 h-[13.50px] left-[1px] top-[1px] absolute" src="https://via.placeholder.com/14x13" />
        </div>
        <div className="w-[423px] h-[82px] relative">
          <div className="left-0 top-[1px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">Зустріч з блогером: </div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-base font-normal font-['Inter']">Корнічук Наталія</div>
          <div className="left-0 top-[33px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Дата і час:</div>
          <div className="left-[205px] top-[33px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">9 березня 2024</div>
          <div className="left-0 top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Відповідальні особи:</div>
          <div className="left-[180px] top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Анна Волошина, Галина Карпінська</div>
          <div className="w-5 h-5 left-[180px] top-[31px] absolute">
            <div className="w-[17.51px] h-[17.50px] left-[1.25px] top-[1.25px] absolute">
            </div>
          </div>
          <div className="left-[356px] top-[33px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">12.30</div>
          <div className="w-5 h-5 left-[331px] top-[31px] absolute">
            <div className="w-[15.62px] h-[17.50px] left-[2.50px] top-[1.25px] absolute">
            </div>
          </div>
        </div>
        <div className="w-[322px] h-[81px] relative">
          <div className="left-0 top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Статус:</div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Планується</div>
          <div className="left-0 top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Посилання і документи:</div>
          <div className="left-[180px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter'] underline">Презентація</div>
          <div className="left-0 top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Коментарі і нотатки:</div>
          <div className="left-[180px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Підготувати питання</div>
        </div>
      </div>
      <div className="w-[298px] h-[321px] left-0 top-0 absolute bg-[#eff7ff] rounded-[20px] shadow">
        <div className="w-[308px] px-6 left-[-5px] top-[24px] absolute justify-start items-center gap-[102px] inline-flex">
          <div className="grow shrink basis-0 text-center"><span className="text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase tracking-tight">Березень</span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter'] tracking-tight"> 2024</span></div>
        </div>
        <div className="w-[251px] h-[0px] left-[23px] top-[61px] absolute border border-[#e4e5e7]"></div>
        <div className="w-[308px] px-6 left-[-5px] top-[77px] absolute justify-between items-start inline-flex">
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">пн</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">вт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">ср</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">чт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">пт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">сб</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">нд</div>
          </div>
        </div>
        <div className="h-[184px] left-[-5px] top-[113px] absolute flex-col justify-start items-start gap-4 inline-flex">
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">2</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">3</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">4</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">5</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">6</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">7</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">8</div>
            </div>
            <div className="w-6 h-6 relative bg-[#f6a801] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-white text-sm font-normal font-['Inter'] uppercase">9</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">10</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">11</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">12</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">13</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">14</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">15</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">16</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">17</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">18</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">19</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">20</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">21</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">22</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">23</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">24</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">25</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">26</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">27</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">28</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">29</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">30</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">31</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
          </div>
        </div>
      </div>
      <div className="w-8 h-8 left-[647px] top-[307px] absolute origin-top-left -rotate-90" />
    </div>
  </div>
  <div className="h-[331px] relative">
    <div className="w-[930px] h-[321px] left-0 top-0 absolute">
      <div className="w-[597px] h-[321px] p-5 left-[333px] top-0 absolute bg-[#eff7ff] rounded-[20px] flex-col justify-start items-start gap-[38px] inline-flex">
        <div className="w-4 h-4 relative">
          <img className="w-3.5 h-[13.50px] left-[1px] top-[1px] absolute" src="https://via.placeholder.com/14x13" />
        </div>
        <div className="w-[422px] h-[82px] relative">
          <div className="left-[1px] top-[1px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">Рекламна інтеграція</div>
          <div className="left-[182px] top-0 absolute text-[#3c3c3c] text-base font-normal font-['Inter']">WOW NAME -інтернет магазин</div>
          <div className="left-[1px] top-[33px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Дата і час:</div>
          <div className="left-[207px] top-[31px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">9 березня 2024</div>
          <div className="left-[357px] top-[31px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">18.00</div>
          <div className="left-0 top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Відповідальні особи:</div>
          <div className="w-5 h-5 left-[182px] top-[30px] absolute">
            <div className="w-[17.51px] h-[17.50px] left-[1.25px] top-[1.25px] absolute">
            </div>
          </div>
          <div className="w-5 h-5 left-[332px] top-[30px] absolute">
            <div className="w-[15.62px] h-[17.50px] left-[2.50px] top-[1.25px] absolute">
            </div>
          </div>
        </div>
        <div className="w-[485px] h-[81px] relative">
          <div className="left-0 top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Статус:</div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Планується</div>
          <div className="left-[1px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Посилання і документи:</div>
          <div className="left-[180px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter'] underline">Презентація</div>
          <div className="left-[1px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Коментарі і нотатки:</div>
          <div className="left-[180px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Знижка при замовленні за промокодом Pink</div>
        </div>
      </div>
      <div className="w-[597px] h-[321px] p-5 left-[333px] top-0 absolute bg-[#eff7ff] rounded-[20px] flex-col justify-start items-start gap-[30px] inline-flex">
        <div className="w-4 h-4 relative">
          <img className="w-3.5 h-[13.50px] left-[1px] top-[1px] absolute" src="https://via.placeholder.com/14x13" />
        </div>
        <div className="w-[423px] h-[82px] relative">
          <div className="left-0 top-[1px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">Зустріч з фотографом: </div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-base font-normal font-['Inter']">Даніл Шевчук</div>
          <div className="left-0 top-[33px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Дата і час:</div>
          <div className="left-[205px] top-[33px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">5 березня 2024</div>
          <div className="left-0 top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Відповідальні особи:</div>
          <div className="left-[180px] top-[65px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Анна Волошина, Галина Карпінська</div>
          <div className="w-5 h-5 left-[180px] top-[31px] absolute">
            <div className="w-[17.51px] h-[17.50px] left-[1.25px] top-[1.25px] absolute">
            </div>
          </div>
          <div className="left-[356px] top-[33px] absolute text-[#3c3c3c] text-sm font-bold font-['Inter']">12.30</div>
          <div className="w-5 h-5 left-[331px] top-[31px] absolute">
            <div className="w-[15.62px] h-[17.50px] left-[2.50px] top-[1.25px] absolute">
            </div>
          </div>
        </div>
        <div className="w-[322px] h-[81px] relative">
          <div className="left-0 top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Статус:</div>
          <div className="left-[180px] top-0 absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Планується</div>
          <div className="left-0 top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Посилання і документи:</div>
          <div className="left-[180px] top-[32px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter'] underline">Презентація</div>
          <div className="left-0 top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Коментарі і нотатки:</div>
          <div className="left-[180px] top-[64px] absolute text-[#3c3c3c] text-sm font-normal font-['Inter']">Підготувати питання</div>
        </div>
      </div>
      <div className="w-[298px] h-[321px] left-0 top-0 absolute bg-[#eff7ff] rounded-[20px] shadow">
        <div className="w-[308px] px-6 left-[-5px] top-[24px] absolute justify-start items-center gap-[102px] inline-flex">
          <div className="grow shrink basis-0 text-center"><span className="text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase tracking-tight">квітень</span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter'] tracking-tight"> 2024</span></div>
        </div>
        <div className="w-[251px] h-[0px] left-[23px] top-[61px] absolute border border-[#e4e5e7]"></div>
        <div className="w-[308px] px-6 left-[-5px] top-[77px] absolute justify-between items-start inline-flex">
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">пн</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">вт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">ср</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">чт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">пт</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">сб</div>
          </div>
          <div className="h-5 p-1 justify-start items-start gap-2.5 flex">
            <div className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">нд</div>
          </div>
        </div>
        <div className="h-[184px] left-[-5px] top-[113px] absolute flex-col justify-start items-start gap-4 inline-flex">
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">2</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">3</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">4</div>
            </div>
            <div className="w-6 h-6 relative bg-[#f6a801] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-white text-sm font-normal font-['Inter'] uppercase">5</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">6</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">7</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">8</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">9</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">10</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">11</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">12</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">13</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">14</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">15</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">16</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">17</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">18</div>
            </div>
            <div className="w-6 h-6 relative bg-[#009d9f] rounded-[40px]">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#fcfcfc] text-sm font-normal font-['Inter'] uppercase">19</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">20</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">21</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">22</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">23</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">24</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">25</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">26</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">27</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">28</div>
            </div>
          </div>
          <div className="self-stretch px-6 justify-between items-start inline-flex">
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">29</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">30</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c] text-sm font-normal font-['Inter'] uppercase">31</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
            <div className="w-6 h-6 relative">
              <div className="w-6 h-6 left-0 top-0 absolute text-center text-[#3c3c3c]/0 text-sm font-normal font-['Inter'] uppercase">1</div>
            </div>
          </div>
        </div>
      </div>
      <div className="w-8 h-8 left-[647px] top-[307px] absolute origin-top-left -rotate-90" />
    </div>
  </div>
</div>
                          </div>
                      </div>
                </div>
            </div>
        </div>
        
    );
   
};

export default  CalendarPage;