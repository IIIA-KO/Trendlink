import { FunctionComponent } from "react";
import {ClassNameType} from "../types/ClassNameType";

const Component4: FunctionComponent<ClassNameType> = ({ className = "" }) => {
  return (
    <div
      className={`absolute top-[0px] left-[2px] w-full h-full flex flex-row items-start justify-start ${className}`}
    >
      <div className="self-stretch flex-1 relative rounded-21xl bg-main-green" />
    </div>
  );
};

export default Component4;
