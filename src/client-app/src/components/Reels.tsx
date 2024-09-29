import { FunctionComponent } from "react";
import FrameComponent4 from "./FrameComponent4";
import FrameComponent3 from "./FrameComponent3";

const Reels: FunctionComponent = () => {
  return (
    <div className="w-full h-[84.375rem] relative bg-[#f0f4f9] overflow-hidden flex flex-col items-end justify-start pt-[2.187rem] px-[6.312rem] pb-[9.937rem] box-border gap-[18.937rem] leading-[normal] tracking-[normal] lg:gap-[9.438rem] lg:pl-[3.125rem] lg:pr-[3.125rem] lg:box-border mq450:gap-[2.375rem] mq750:gap-[4.75rem] mq750:pl-[1.563rem] mq750:pr-[1.563rem] mq750:box-border mq1050:h-auto">
      <div className="w-[23.938rem] h-[2.188rem] rounded-[40px] bg-[#fff] flex flex-row items-center justify-start py-[0.437rem] px-[0.937rem] box-border max-w-full z-[1]">
        <img
          className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
          loading="lazy"
          alt=""
          src="/src/assets/materialsymbolslightsearch.svg"
        />
      </div>
      <main className="flex flex-row items-start justify-end pt-[0rem] px-[2.75rem] pb-[10.25rem] box-border max-w-full mq750:pb-[4.375rem] mq750:box-border mq1050:pl-[1.375rem] mq1050:pr-[1.375rem] mq1050:pb-[6.688rem] mq1050:box-border">
        <section className="flex-1 flex flex-col items-end justify-start gap-[3.125rem] max-w-full text-left text-[0.875rem] text-[#3c3c3c] font-[Inter] mq450:gap-[1.563rem]">
          <div className="self-stretch flex flex-row items-start justify-start gap-[0.25rem] mq1050:flex-wrap">
            <div className="flex-[0.5309] border-[#c0bebe] border-b-[1px] border-solid box-border overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[4.375rem] pr-[4.312rem] min-w-[7.375rem] z-[3] mq450:flex-1">
              <div className="relative inline-block min-w-[2.688rem]">
                Огляд
              </div>
            </div>
            <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.937rem] pr-[3.875rem] z-[1]">
              <div className="relative inline-block min-w-[3.563rem]">
                {" "}
                Відгуки
              </div>
            </div>
            <div className="flex-1 border-[#009ea0] border-b-[2px] border-solid box-border overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.5rem] pl-[3.187rem] pr-[3.125rem] min-w-[7.375rem] z-[1]">
              <div className="relative inline-block min-w-[5.063rem]">
                Статистика
              </div>
            </div>
            <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.625rem] pr-[3.562rem] z-[1] text-[#000]">
              <div className="relative">Написати</div>
            </div>
            <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[2.187rem] pr-[2.125rem] z-[1]">
              <div className="relative inline-block min-w-[7.063rem]">
                Умови співпраці
              </div>
            </div>
          </div>
          <FrameComponent4 />
        </section>
      </main>
      <img
        className="w-[7.5rem] h-[68.75rem] absolute !m-[0] top-[2.25rem] left-[6.25rem]"
        alt=""
        src="/src/assets/component-39.svg"
      />
      <FrameComponent3
        profilePicture="/ellipse-11@2x.png"
        group95="/group-95.svg"
        propTextDecoration="none"
        prop="Зберегти"
        editButtonBorderRadius="40px"
        editButtonBackgroundColor="#009ea0"
        editButtonOverflow="hidden"
        editButtonDisplay="flex"
        editButtonFlexDirection="row"
        editButtonPadding="0.625rem 4rem 0.625rem 4.062rem"
        editButtonPosition="absolute"
        editButtonTop="11.625rem"
        editButtonLeft="45.75rem"
        divDisplay="inline-block"
        divMinWidth="4.125rem"
      />
      <div className="w-[67.125rem] flex flex-row items-start justify-center max-w-full">
        <img
          className="h-[1.5rem] w-[1.5rem] relative object-contain"
          alt=""
          src="/src/assets/actions--navigation--chevronright--24@2x.png"
        />
      </div>
    </div>
  );
};

export default Reels;
