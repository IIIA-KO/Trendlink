import { FunctionComponent } from "react";
import "antd/dist/reset.css";
import { Input } from "antd";

export type FrameComponentType = {
  className?: string;
};

const FrameComponent: FunctionComponent<FrameComponentType> = ({
  className = "",
}) => {
  return (
    <div
      className={`self-stretch flex flex-col items-start justify-start gap-1 text-left text-xs text-text-2 font-inter ${className}`}
    >
      <div className="flex flex-row items-start justify-start py-0 px-2">
        <i className="relative font-light z-[1]">Посилання і документи</i>
      </div>
      <div className="self-stretch flex flex-row items-start justify-start gap-2.5 mq450:flex-wrap">
        <div className="flex-1 flex flex-col items-start justify-start gap-[18px] min-w-[194px]">
          <Input
            className="bg-[transparent] self-stretch h-[35px] relative z-[1]"
            bordered={true}
          />
          <Input
            className="bg-[transparent] self-stretch font-inter italic font-light text-xs text-text-2"
            bordered={true}
          />
        </div>
        <div className="flex flex-col items-start justify-start pt-1 px-0 pb-0">
          <img
            className="w-6 h-6 relative object-cover z-[6]"
            loading="lazy"
            alt=""
            src="/actions-2@2x.png"
          />
        </div>
      </div>
    </div>
  );
};

export default FrameComponent;
