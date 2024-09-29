import { FunctionComponent, memo } from "react";

export type Component7Type = {
  className?: string;
};

const Component7: FunctionComponent<Component7Type> = memo(
  ({ className = "" }) => {
    return (
      <div
        className={`!m-[0] absolute top-[22.375rem] right-[1rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[3rem] z-[4] text-center text-[0.688rem] text-[#3c3c3c] font-[Inter] ${className}`}
      >
        <div className="h-[4.875rem] w-[6.063rem] rounded-[5px] border-[#c0bebe] border-[1px] border-solid box-border hidden flex-col items-start justify-start">
          <div className="self-stretch flex-1 flex flex-col items-start justify-center py-[0rem] px-[0.75rem]">
            <div className="relative">14 днів</div>
          </div>
          <div className="self-stretch flex-1 flex flex-col items-start justify-center py-[0rem] px-[0.75rem]">
            <div className="relative">21 днів</div>
          </div>
          <div className="self-stretch flex-1 flex flex-col items-start justify-center py-[0rem] px-[0.75rem]">
            <div className="relative">7 днів</div>
          </div>
        </div>
        <div className="flex flex-row items-start justify-start py-[0.468rem] px-[0rem] gap-[0.375rem] text-left text-[0.75rem] text-[#7c7c7c]">
          <div className="relative inline-block min-w-[6.063rem] shrink-0">
            За останні 7днів
          </div>
          <img
            className="h-[0.938rem] w-[0.938rem] relative shrink-0"
            alt=""
            src="/actions--navigation--chevrondown--201.svg"
          />
        </div>
      </div>
    );
  }
);

export default Component7;
