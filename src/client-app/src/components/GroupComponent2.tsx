import { FunctionComponent } from "react";
import Link from "./Link";

export type GroupComponent2Type = {
  className?: string;
};

const GroupComponent2: FunctionComponent<GroupComponent2Type> = ({
  className = "",
}) => {
  return (
    <div
      className={`absolute top-[1005px] left-[195px] rounded-xl bg-aliceblue-200 w-[598px] flex flex-row items-end justify-start flex-wrap content-end pt-[26px] px-[52px] pb-8 box-border gap-[17px] max-w-full z-[1] text-left text-base text-main-black font-inter ${className}`}
    >
      <div className="h-[246px] w-[598px] relative rounded-xl bg-aliceblue-200 hidden max-w-full" />
      <div className="flex-1 flex flex-col items-start justify-start gap-[30px] max-w-full">
        <div className="w-[413px] flex flex-row items-start justify-between max-w-full gap-5 mq450:flex-wrap">
          <div className="flex flex-col items-start justify-start pt-2 px-0 pb-0">
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
            propMinWidth="44px"
            vector1="/vector-1.svg"
            propWidth="44px"
            propPosition2="relative"
            propMargin1="unset"
            propTop1="unset"
            propLeft1="unset"
          />
        </div>
        <div className="self-stretch flex flex-row items-start justify-start gap-[13px] max-w-full text-sm mq700:flex-wrap">
          <div className="flex flex-col items-start justify-start gap-[20.3px]">
            <div className="relative inline-block min-w-[30px] z-[1]">Київ</div>
            <div className="relative inline-block min-w-[43px] z-[1]">
              Луцьк
            </div>
            <div className="relative inline-block min-w-[56px] z-[1]">
              Чернігів
            </div>
            <div className="relative inline-block min-w-[59px] z-[1]">
              Чернівці
            </div>
          </div>
          <div className="flex-1 flex flex-col items-start justify-start gap-[14.3px] min-w-[228px] max-w-full">
            <div className="self-stretch h-[22px] relative bg-whitesmoke-300 z-[1]">
              <div className="absolute top-[0px] left-[0px] bg-whitesmoke-300 w-full h-full hidden z-[1]" />
              <div className="absolute top-[0px] left-[0px] bg-sandybrown w-[246px] h-[22px] z-[2]" />
            </div>
            <div className="self-stretch h-[22px] relative bg-whitesmoke-300 z-[1]">
              <div className="absolute top-[0px] left-[0px] bg-whitesmoke-300 w-full h-full hidden z-[1]" />
              <div className="absolute top-[0px] left-[0px] bg-sandybrown w-24 h-[22px] z-[2]" />
            </div>
            <div className="self-stretch h-[22px] relative bg-whitesmoke-300 z-[1]">
              <div className="absolute top-[0px] left-[0px] bg-whitesmoke-300 w-full h-full hidden z-[1]" />
              <div className="absolute top-[0px] left-[0px] bg-sandybrown w-[52px] h-[22px] z-[2]" />
            </div>
            <div className="self-stretch h-[22px] relative bg-whitesmoke-300 z-[1]">
              <div className="absolute top-[0px] left-[0px] bg-whitesmoke-300 w-full h-full hidden z-[1]" />
              <div className="absolute top-[0px] left-[0px] bg-sandybrown w-[76px] h-[22px] z-[2]" />
            </div>
          </div>
        </div>
      </div>
      <div className="w-[52px] flex flex-col items-start justify-start gap-9">
        <Link prop="Країни" vector1="pending_I963:7255;963:7221" />
        <div className="flex flex-row items-start justify-start py-0 px-0.5">
          <div className="flex flex-col items-start justify-start gap-[16.3px]">
            <div className="relative inline-block min-w-[36px] z-[1]">60%</div>
            <div className="relative inline-block min-w-[36px] z-[1]">30%</div>
            <div className="relative inline-block min-w-[36px] z-[1]">20%</div>
            <div className="relative inline-block min-w-[35px] z-[1]">25%</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GroupComponent2;
