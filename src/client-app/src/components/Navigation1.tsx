import { FunctionComponent } from "react";
import RectangleComponent from "./RectangleComponent";

export type Navigation1Type = {
  className?: string;
};

const Navigation1: FunctionComponent<Navigation1Type> = ({
  className = "",
}) => {
  return (
    <div
      className={`h-[1100px] w-[120px] relative text-center text-sm text-black font-logo-font mq750:hidden ${className}`}
    >
      <RectangleComponent />
      <div className="absolute top-[35px] left-[25px] w-[70px] h-[70px]">
        <div className="absolute top-[0px] left-[0px] rounded-[50%] bg-white w-full h-full z-[1]" />
        <a className="[text-decoration:none] absolute top-[18px] left-[8px] font-bold text-[inherit] inline-block w-[54px] h-[34px] z-[2]">
          <p className="m-0">Trend</p>
          <p className="m-0">Link</p>
        </a>
      </div>
    </div>
  );
};

export default Navigation1;
