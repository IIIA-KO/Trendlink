import { FunctionComponent } from "react";
import SearchInputContainer from "../components/SearchInputContainer";
import FrameComponent3 from "../components/FrameComponent3";

const Frame2: FunctionComponent = () => {
  return (
    <div className="w-full h-[113.25rem] relative bg-[#f0f4f9] overflow-hidden flex flex-col items-end justify-start pt-[2.187rem] px-[6.312rem] pb-[9.5rem] box-border gap-[18.937rem] leading-[normal] tracking-[normal] lg:gap-[9.438rem] lg:pl-[3.125rem] lg:pr-[3.125rem] lg:box-border mq450:gap-[2.375rem] mq750:gap-[4.75rem] mq750:pl-[1.563rem] mq750:pr-[1.563rem] mq750:box-border mq1050:h-auto">
      <div className="w-[23.938rem] h-[2.188rem] rounded-[40px] bg-[#fff] flex flex-row items-center justify-start py-[0.437rem] px-[0.937rem] box-border max-w-full z-[1]">
        <img
          className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
          loading="lazy"
          alt=""
          src="/src/assets/materialsymbolslightsearch.svg"
        />
      </div>
      <main className="w-[63.375rem] flex flex-row items-start justify-end pt-[0rem] px-[2.687rem] pb-[9.812rem] box-border max-w-full lg:pb-[6.375rem] lg:box-border mq750:pb-[4.125rem] mq750:box-border mq1050:pl-[1.313rem] mq1050:pr-[1.313rem] mq1050:box-border">
        <SearchInputContainer />
      </main>
      <img
        className="w-[7.5rem] h-[68.75rem] absolute !m-[0] top-[2.25rem] left-[6.25rem]"
        alt=""
        src="/src/assets/component-39.svg"
      />
      <FrameComponent3
        profilePicture="/ellipse-11@2x.png"
        group95="/group-95.svg"
        propTextDecoration="unset"
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

export default Frame2;
