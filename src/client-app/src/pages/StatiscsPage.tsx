import { FunctionComponent } from "react";
import MenuTabs from "../components/MenuTabs";
import FrameComponent3 from "../components/FrameComponent3";
import Link from "../components/Link";

const Frame: FunctionComponent = () => {
  return (
    <div className="w-full h-[81.125rem] relative bg-[#f0f4f9] overflow-hidden flex flex-col items-end justify-start pt-[2.187rem] pb-[12.375rem] pl-[6.25rem] pr-[6.312rem] box-border gap-[31.625rem] leading-[normal] tracking-[normal] text-left text-[1rem] text-[#3c3c3c] font-[Inter] mq450:gap-[7.875rem] mq450:pl-[1.25rem] mq450:pr-[1.25rem] mq450:box-border mq750:h-auto mq750:gap-[15.813rem] mq750:pl-[3.125rem] mq750:pr-[3.125rem] mq750:box-border">
      <main className="self-stretch flex flex-col items-end justify-start gap-[7.687rem] shrink-0 max-w-full lg:gap-[3.813rem] mq450:gap-[0.938rem] mq750:gap-[1.938rem]">
        <div className="w-[23.938rem] rounded-[40px] bg-[#fff] flex flex-row items-center justify-start py-[0.437rem] px-[0.937rem] box-border max-w-full z-[1]">
          <img
            className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/src/assets/materialsymbolslightsearch.svg"
          />
        </div>
        <section className="self-stretch flex flex-row items-start justify-start max-w-full ">
          <MenuTabs />
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
      <div className="w-[37.375rem] !m-[0] absolute bottom-[12.375rem] left-[18.438rem] rounded-[20px] bg-[#f0f4f9] flex flex-row items-end justify-start pt-[1.625rem] px-[3.25rem] pb-[2rem] box-border gap-[1.062rem] max-w-full z-[3] mq750:flex-wrap">
        <div className="h-[15.375rem] w-[37.375rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
        <div className="flex-1 flex flex-col items-start justify-start gap-[1.875rem] max-w-full">
          <div className="w-[25.813rem] flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq450:flex-wrap">
            <div className="flex flex-col items-start justify-start pt-[0.5rem] px-[0rem] pb-[0rem]">
              <b className="relative uppercase z-[1]">Топ місцезнаходження</b>
            </div>
            <Link
              propAlignSelf="unset"
              propHeight="unset"
              propPosition="unset"
              prop="Міста"
              propPosition1="relative"
              propMargin="unset"
              propTop="unset"
              propLeft="unset"
              propMinWidth="unset"
              vector1="/vector-1.svg"
              propWidth="2.75rem"
              propPosition2="relative"
              propMargin1="unset"
              propTop1="unset"
              propLeft1="unset"
            />
          </div>
          <div className="self-stretch flex flex-row items-start justify-start gap-[0.812rem] max-w-full text-[0.875rem] mq750:flex-wrap">
            <div className="flex flex-col items-start justify-start gap-[1.268rem]">
              <div className="relative inline-block min-w-[1.938rem] z-[1]">
                Київ
              </div>
              <div className="relative inline-block min-w-[2.75rem] z-[1]">
                Луцьк
              </div>
              <div className="relative z-[1]">Чернігів</div>
              <div className="relative inline-block min-w-[3.75rem] z-[1]">
                Чернівці
              </div>
            </div>
            <div className="flex-1 flex flex-col items-start justify-start gap-[0.893rem] min-w-[14.25rem] max-w-full">
              <div className="self-stretch h-[1.375rem] relative bg-[#eaeaea] z-[1]">
                <div className="absolute top-[0rem] left-[0rem] bg-[#eaeaea] w-full h-full hidden z-[1]" />
                <div className="absolute top-[0rem] left-[0rem] bg-[#f3ae5f] w-[15.375rem] h-[1.375rem] z-[2]" />
              </div>
              <div className="self-stretch h-[1.375rem] relative bg-[#eaeaea] z-[1]">
                <div className="absolute top-[0rem] left-[0rem] bg-[#eaeaea] w-full h-full hidden z-[1]" />
                <div className="absolute top-[0rem] left-[0rem] bg-[#f3ae5f] w-[6rem] h-[1.375rem] z-[2]" />
              </div>
              <div className="self-stretch h-[1.375rem] relative bg-[#eaeaea] z-[1]">
                <div className="absolute top-[0rem] left-[0rem] bg-[#eaeaea] w-full h-full hidden z-[1]" />
                <div className="absolute top-[0rem] left-[0rem] bg-[#f3ae5f] w-[3.25rem] h-[1.375rem] z-[2]" />
              </div>
              <div className="self-stretch h-[1.375rem] relative bg-[#eaeaea] z-[1]">
                <div className="absolute top-[0rem] left-[0rem] bg-[#eaeaea] w-full h-full hidden z-[1]" />
                <div className="absolute top-[0rem] left-[0rem] bg-[#f3ae5f] w-[4.75rem] h-[1.375rem] z-[2]" />
              </div>
            </div>
          </div>
        </div>
        <div className="w-[3.25rem] flex flex-col items-start justify-start gap-[2.25rem]">
          <Link
            propAlignSelf="stretch"
            propHeight="1.313rem"
            propPosition="relative"
            prop="Країни"
            propPosition1="absolute"
            propMargin="0 !important"
            propTop="0rem"
            propLeft="0rem"
            propMinWidth="3.313rem"
            vector1="pending_I1428:17400;963:7221"
            propWidth="0rem"
            propPosition2="absolute"
            propMargin1="0 !important"
            propTop1="1.313rem"
            propLeft1="0rem"
          />
          <div className="flex flex-row items-start justify-start py-[0rem] px-[0.125rem]">
            <div className="flex flex-col items-start justify-start gap-[1.018rem]">
              <div className="relative inline-block min-w-[2.313rem] z-[1]">
                60%
              </div>
              <div className="relative inline-block min-w-[2.313rem] z-[1]">
                30%
              </div>
              <div className="relative inline-block min-w-[2.313rem] z-[1]">
                20%
              </div>
              <div className="relative inline-block min-w-[2.25rem] z-[1]">
                25%
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="w-[67.125rem] flex flex-row items-start justify-center max-w-full">
        <img
          className="h-[1.5rem] w-[1.5rem] relative object-contain shrink-0"
          alt=""
          src="/src/assets/actions--navigation--chevronright--24@2x.png"
        />
      </div>
    </div>
  );
};

export default Frame;
