import { FunctionComponent } from "react";

export type ExistingEmailConfirmationType = {
  className?: string;
};

const ExistingEmailConfirmation: FunctionComponent<
  ExistingEmailConfirmationType
> = ({ className = "" }) => {
  return (
    <div
      className={`self-stretch flex flex-col items-start justify-start gap-9 max-w-full text-center text-5xl text-main-black font-inter mq750:gap-[18px] ${className}`}
    >
      <div className="self-stretch flex flex-row items-start justify-start py-0 pl-0.5 pr-0 box-border max-w-full">
        <div className="flex-1 flex flex-row items-end justify-start max-w-full [row-gap:20px] mq750:flex-wrap">
          <div className="flex-1 rounded-8xs bg-white border-silver-100 border-[2px] border-solid box-border flex flex-row items-start justify-center pt-[9px] pb-2.5 pl-[21px] pr-5 min-w-[117px] z-[2]">
            <div className="h-[54px] w-[180px] relative rounded-8xs bg-white border-silver-100 border-[2px] border-solid box-border hidden" />
            <div className="w-[11px] relative font-light inline-block min-w-[11px] z-[1] mq450:text-lgi">
              1
            </div>
          </div>
          <div className="w-[119px] flex flex-col items-start justify-end pt-0 px-0 pb-6 box-border">
            <div className="self-stretch h-0.5 relative border-white border-t-[2px] border-solid box-border z-[3]" />
          </div>
          <div className="flex-1 rounded-8xs bg-white flex flex-row items-start justify-center pt-3 pb-[13px] pl-[21px] pr-5 box-border min-w-[117px] z-[2]">
            <div className="h-[54px] w-[180px] relative rounded-8xs bg-white hidden" />
            <div className="relative font-light inline-block min-w-[15px] z-[1] mq450:text-lgi">
              2
            </div>
          </div>
        </div>
      </div>
      <h2 className="m-0 relative text-xl font-normal font-[inherit] z-[2] mq450:text-base">
        Підтвердити існуючий Email
      </h2>
    </div>
  );
};

export default ExistingEmailConfirmation;
