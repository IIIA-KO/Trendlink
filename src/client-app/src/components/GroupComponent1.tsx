import { FunctionComponent } from "react";
import "antd/dist/reset.css";
import { Input } from "antd";

export type GroupComponent1Type = {
  className?: string;
};

const GroupComponent1: FunctionComponent<GroupComponent1Type> = ({
  className = "",
}) => {
  return (
    <div
      className={`absolute top-[742px] left-[194px] rounded-xl bg-aliceblue-200 w-[598px] flex flex-col items-start justify-start py-7 pl-[55px] pr-[53px] box-border gap-[21.5px] max-w-full z-[4] text-left text-xs text-main-black font-inter ${className}`}
    >
      <div className="w-[598px] h-[233px] relative rounded-xl bg-aliceblue-200 hidden max-w-full" />
      <b className="relative text-base uppercase inline-block min-w-[27px] z-[1]">
        Вік
      </b>
      <div className="self-stretch flex flex-row items-start justify-start py-0 px-px box-border max-w-full">
        <div className="flex-1 flex flex-row items-start justify-start flex-wrap content-start gap-3 max-w-full">
          <div className="flex-1 flex flex-col items-start justify-start pt-2 px-0 pb-0 box-border min-w-[292px] max-w-full">
            <div className="self-stretch h-[0.5px] relative border-transparent border-t-[0.5px] border-solid box-border z-[1]" />
          </div>
          <div className="relative inline-block min-w-[27px] z-[1]">60%</div>
        </div>
      </div>
      <div className="self-stretch flex flex-row items-start justify-start py-0 pl-px pr-0 box-border max-w-full">
        <div className="flex-1 flex flex-row items-start justify-start flex-wrap content-start gap-3 max-w-full">
          <div className="flex-1 flex flex-col items-start justify-start pt-2 px-0 pb-0 box-border min-w-[292px] max-w-full">
            <div className="self-stretch flex flex-col items-start justify-start gap-[34.5px] max-w-full">
              <div className="self-stretch h-[0.5px] relative border-transparent border-t-[0.5px] border-solid box-border z-[1]" />
              <div className="self-stretch flex flex-col items-end justify-start gap-[7.5px] max-w-full">
                <Input
                  className="bg-[transparent] self-stretch"
                  bordered={true}
                />
                <div className="self-stretch flex flex-row items-start justify-end py-0 pl-[18px] pr-[9px] box-border max-w-full">
                  <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-5 mq700:flex-wrap">
                    <div className="relative inline-block min-w-[41px] z-[1]">
                      18-24р
                    </div>
                    <div className="relative inline-block min-w-[43px] z-[1]">
                      25-34р
                    </div>
                    <div className="relative inline-block min-w-[44px] z-[1]">
                      35-44р
                    </div>
                    <div className="relative inline-block min-w-[43px] z-[1]">
                      45-54р
                    </div>
                    <div className="relative inline-block min-w-[43px] z-[1]">
                      55-64р
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="flex flex-col items-start justify-start gap-[21px]">
            <div className="relative inline-block min-w-[28px] z-[1]">40%</div>
            <div className="relative inline-block min-w-[27px] z-[1]">20%</div>
            <div className="relative inline-block min-w-[20px] z-[1]">0%</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GroupComponent1;
