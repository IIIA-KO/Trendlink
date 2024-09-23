import { FunctionComponent } from "react";

export type RectangleComponentType = {
  className?: string;
};

const RectangleComponent: FunctionComponent<RectangleComponentType> = ({
  className = "",
}) => {
  return (
    <div
      className={`absolute top-[0px] left-[-30px] w-full h-full overflow-hidden flex flex-row items-start justify-start ${className}`}
    >
      <div className="mt-[-10px] ml-[-20px] h-[1120px] w-[344px] flex flex-row items-start justify-start p-2.5 box-border shrink-0 max-w-[287%]">
        <div className="self-stretch w-[120px] relative rounded-21xl bg-main-green" />
      </div>
    </div>
  );
};

export default RectangleComponent;
