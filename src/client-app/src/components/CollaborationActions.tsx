import { FunctionComponent } from "react";
import "antd/dist/reset.css";
import { Button } from "antd";

export type CollaborationActionsType = {
  className?: string;
};

const CollaborationActions: FunctionComponent<CollaborationActionsType> = ({
  className = "",
}) => {
  return (
    <div
      className={`self-stretch rounded-t-none rounded-b-xl bg-gainsboro-100 flex flex-row items-start justify-between pt-[27px] pb-[33px] pl-[45px] pr-11 box-border max-w-full gap-5 z-[1] mq750:flex-wrap ${className}`}
    >
      <div className="h-[97px] w-[867px] relative rounded-t-none rounded-b-xl bg-gainsboro-100 hidden max-w-full" />
      <button className="cursor-pointer [border:none] py-2.5 px-[33px] bg-main-green rounded-21xl overflow-hidden flex flex-row items-start justify-start whitespace-nowrap z-[2]">
        <div className="relative text-sm font-inter text-white text-left inline-block min-w-[128px]">
          Додати співпрацю
        </div>
      </button>
      <Button className="whitespace-nowrap z-[2]" type="primary" />
    </div>
  );
};

export default CollaborationActions;
