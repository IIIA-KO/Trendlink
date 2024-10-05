import { FunctionComponent } from "react";
import "antd/dist/reset.css";
import { Input, DatePicker, Dropdown, Menu, Button } from "antd";
import { DownOutlined } from "@ant-design/icons";
import FrameComponent from "./FrameComponent";
import Button1 from "./Buttons/Button";
import CollaborationActions from "./CollaborationActions";
import {ClassNameType} from "../types/ClassNameType";

interface MenuOption {
  value: string;
}

const menuOptions: MenuOption[] = [
  { value: 'Option 1' },
  { value: 'Option 2' },
  { value: 'Option 3' },
];

const GroupComponent3: FunctionComponent<ClassNameType> = ({
  className = "",
}) => {
  return (
    <div
      className={`flex-1 shadow-[3px_3px_10px_2px_rgba(108,_108,_108,_0.2)] rounded-xl bg-whitesmoke-100 flex flex-col items-end justify-start pt-5 px-0 pb-0 box-border gap-[61px] max-w-full z-[5] text-center text-sm text-black font-inter ${className}`}
    >
      <img
        className="w-[15px] h-[15px] relative object-cover hidden"
        alt=""
        src="/src/assets/organization--status--information--24@2x.png"
      />
      <div className="relative hidden">найближча інтеграція 1.03.24</div>
      <div className="self-stretch h-[550px] relative shadow-[3px_3px_10px_2px_rgba(108,_108,_108,_0.2)] rounded-xl bg-whitesmoke-100 hidden" />
      <div className="w-[842px] flex flex-row items-start justify-end py-0 px-5 box-border max-w-full text-left text-xs text-text-2">
        <div className="flex-1 flex flex-col items-end justify-start gap-2.5 max-w-full">
          <img
            className="w-[25px] h-[25px] relative object-cover z-[1]"
            loading="lazy"
            alt=""
            src="/src/assets/actions@2x.png"
          />
          <div className="self-stretch flex flex-row items-start justify-end py-0 pl-0 pr-6 box-border max-w-full">
            <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-5 mq1050:flex-wrap">
              <div className="w-[333px] flex flex-col items-start justify-start gap-[86px] min-w-[333px] max-w-full mq1050:flex-1">
                <div className="self-stretch flex flex-col items-start justify-start gap-[15px]">
                  <div className="w-[299px] flex flex-col items-start justify-start gap-1">
                    <div className="flex flex-row items-start justify-start py-0 px-2">
                      <i className="relative inline-block font-light min-w-[30px] z-[1]">
                        Опис
                      </i>
                    </div>
                    <div className="self-stretch rounded-8xs bg-white flex flex-row items-start justify-start py-[9px] px-2 whitespace-nowrap z-[1] text-sm text-main-black">
                      <div className="h-[35px] w-[299px] relative rounded-8xs bg-white hidden" />
                      <b className="relative z-[2]">{`Зустріч з блогером `}</b>
                    </div>
                  </div>
                  <Input
                    className="bg-[transparent] self-stretch font-inter text-sm text-main-black"
                    bordered={true}
                  />
                </div>
                <FrameComponent />
              </div>
              <div className="w-[329px] flex flex-col items-start justify-start gap-[5px] min-w-[329px] max-w-full mq1050:flex-1">
                <div className="self-stretch flex flex-row items-start justify-start gap-[25px] mq450:flex-wrap">
                  <div className="w-[124px] flex flex-col items-start justify-start gap-[38px] min-w-[124px] mq450:flex-1">
                    <div className="self-stretch flex flex-col items-start justify-start gap-[17px]">
                      <DatePicker
                        className="w-[119px]"
                        placeholder="Дата "
                        allowClear={false}
                        bordered={false}
                      />
                      <div className="self-stretch flex flex-row items-start justify-start">
                        <div className="flex-1 flex flex-col items-start justify-start gap-[22px]">
                          <div className="self-stretch flex flex-col items-start justify-start gap-1">
                            <div className="flex flex-row items-start justify-start py-0 pl-[7px] pr-[5px]">
                              <i className="relative inline-block font-light min-w-[45px] z-[1]">
                                Години:
                              </i>
                            </div>
                            <div className="self-stretch rounded-8xs bg-white flex flex-row items-start justify-start pt-2.5 px-2 pb-2 z-[1] text-sm text-transparent">
                              <div className="h-[35px] w-[57px] relative rounded-8xs bg-white hidden" />
                              <div className="relative font-light inline-block min-w-[17px] z-[2]">
                                hh
                              </div>
                            </div>
                          </div>
                          <Button1
                            propRight="51.06%"
                            propLeft="0%"
                            propBackgroundColor="#8da7ad"
                          />
                        </div>
                        <div className="flex-1 flex flex-col items-start justify-start gap-[26px]">
                          <div className="self-stretch flex flex-row items-start justify-start py-0 pl-[5px] pr-0.5">
                            <div className="flex-1 flex flex-col items-start justify-start gap-1">
                              <div className="flex flex-row items-start justify-start py-0 pl-[5px] pr-0">
                                <i className="relative inline-block font-light min-w-[53px] z-[1]">
                                  Хвилини:
                                </i>
                              </div>
                              <div className="self-stretch rounded-8xs bg-white flex flex-row items-start justify-start pt-2.5 px-2 pb-2 z-[1] text-sm text-transparent">
                                <div className="h-[35px] w-[57px] relative rounded-8xs bg-white hidden" />
                                <div className="relative font-light inline-block min-w-[25px] z-[2]">
                                  mm
                                </div>
                              </div>
                            </div>
                          </div>
                          <div className="relative text-sm text-main-black inline-block min-w-[65px] z-[1]">
                            Giveaway
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="flex flex-row items-start justify-start py-0 pl-2 pr-0">
                      <i className="relative inline-block font-light min-w-[116px] z-[1]">
                        Коментарі і нотатки
                      </i>
                    </div>
                  </div>
                  <div className="flex-1 flex flex-col items-start justify-start gap-[17px] min-w-[115px]">
                    <div className="w-24 flex flex-col items-start justify-start gap-1">
                      <div className="flex flex-row items-start justify-start py-0 px-2">
                        <i className="relative inline-block font-light min-w-[17px] z-[1]">
                          Рік
                        </i>
                      </div>
                      <Dropdown
                        className="self-stretch gap-5 z-[1]"
                        overlay={
                          <Menu>
                            {menuOptions.map((option: MenuOption, index: number) => (
                              <Menu.Item key={index}>
                                <a onClick={(e) => e.preventDefault()}>
                                  {option.value || ""}
                                </a>
                              </Menu.Item>
                            ))}
                          </Menu>
                        }
                        trigger={["hover"]}
                      >
                        <Button onClick={(e) => e.preventDefault()}>
                          {` `}
                          <DownOutlined />
                        </Button>
                      </Dropdown>
                    </div>
                    <div className="self-stretch flex flex-col items-start justify-start pt-0 px-0 pb-[5px] gap-1">
                      <div className="flex flex-row items-start justify-start py-0 px-2">
                        <i className="relative inline-block font-light min-w-[41px] z-[1]">
                          Статус
                        </i>
                      </div>
                      <Dropdown
                        className="self-stretch gap-5 z-[1]"
                        overlay={
                          <Menu>
                            {menuOptions.map((option: MenuOption, index: number) => (
                              <Menu.Item key={index}>
                                <a onClick={(e) => e.preventDefault()}>
                                  {option.value || ""}
                                </a>
                              </Menu.Item>
                            ))}
                          </Menu>
                        }
                        trigger={["hover"]}
                      >
                        <Button onClick={(e) => e.preventDefault()}>
                          {` `}
                          <DownOutlined />
                        </Button>
                      </Dropdown>
                    </div>
                    <div className="self-stretch flex flex-row items-start justify-start gap-2.5 text-sm text-main-black">
                      <Button />
                      <div className="flex-1 relative z-[1]">
                        Дата неможлива для співпраці
                      </div>
                    </div>
                  </div>
                </div>
                <textarea className="[border:none] bg-white [outline:none] self-stretch h-[109px] relative rounded-8xs z-[1]" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <CollaborationActions />
    </div>
  );
};

export default GroupComponent3;
