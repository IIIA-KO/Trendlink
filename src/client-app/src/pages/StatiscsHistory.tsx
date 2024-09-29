import { FunctionComponent } from "react";
import ProfileNavigation1 from "../components/ProfileNavigation1";
import FrameComponent3 from "../components/FrameComponent3";

const Frame3: FunctionComponent = () => {
  return (
    <div className="w-full h-[84.375rem] relative bg-[#f0f4f9] overflow-hidden flex flex-col items-end justify-start pt-[2.187rem] pb-[19.812rem] pl-[6.25rem] pr-[6.312rem] box-border gap-[39.062rem] leading-[normal] tracking-[normal] mq750:gap-[19.5rem] mq750:pl-[3.125rem] mq750:pr-[3.125rem] mq750:box-border mq1050:h-auto mq450:gap-[9.75rem] mq450:pl-[1.25rem] mq450:pr-[1.25rem] mq450:box-border">
      <main className="self-stretch flex flex-col items-end justify-start gap-[7.687rem] shrink-0 max-w-full lg:gap-[3.813rem] mq750:gap-[1.938rem] mq450:gap-[0.938rem]">
        <div className="w-[23.938rem] rounded-[40px] bg-[#fff] flex flex-row items-center justify-start py-[0.437rem] px-[0.937rem] box-border max-w-full z-[1]">
          <img
            className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/materialsymbolslightsearch.svg"
          />
        </div>
        <ProfileNavigation1 />
      </main>
      <img
        className="w-[7.5rem] h-[68.75rem] absolute !m-[0] top-[2.25rem] left-[6.25rem]"
        alt=""
        src="/component-39.svg"
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
          className="h-[1.5rem] w-[1.5rem] relative object-contain shrink-0"
          alt=""
          src="/actions--navigation--chevronright--24@2x.png"
        />
      </div>
    </div>
  );
};

export default Frame3;
