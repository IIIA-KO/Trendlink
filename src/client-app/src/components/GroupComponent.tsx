import { FunctionComponent } from "react";

export type GroupComponentType = {
  className?: string;
};

const GroupComponent: FunctionComponent<GroupComponentType> = ({
  className = "",
}) => {
  return (
    <div
      className={`w-[597px] !m-[0] absolute right-[47px] bottom-[0px] rounded-xl bg-aliceblue-100 flex flex-col items-start justify-start pt-6 pb-[47px] pl-[43px] pr-[33px] box-border gap-[71px] max-w-full z-[1] text-left text-sm text-main-black font-inter ${className}`}
    >
      <div className="w-[597px] h-[321px] relative rounded-xl bg-aliceblue-100 hidden max-w-full" />
      <div className="self-stretch flex flex-col items-end justify-start max-w-full">
        <img
          className="w-4 h-4 relative object-cover z-[1]"
          loading="lazy"
          alt=""
          src="/actions--formatting--edit--25@2x.png"
        />
        <div className="self-stretch flex flex-row items-start justify-start max-w-full">
          <div className="flex flex-row items-end justify-start gap-[19px] max-w-full mq750:flex-wrap">
            <div className="flex flex-col items-start justify-start gap-[15px]">
              <div className="flex flex-row items-start justify-start py-0 pl-px pr-0">
                <b className="relative z-[1]">{`Зустріч з фотографом: `}</b>
              </div>
              <div className="flex flex-row items-start justify-start py-0 px-px">
                <div className="relative inline-block min-w-[73px] z-[1]">
                  Дата і час:
                </div>
              </div>
              <div className="relative z-[1]">Відповідальні особи:</div>
            </div>
            <div className="flex flex-col items-start justify-start gap-[15px] text-base">
              <div className="flex flex-col items-start justify-start gap-2.5">
                <div className="relative inline-block min-w-[110px] z-[1]">
                  Даніл Шевчук
                </div>
                <div className="flex flex-row items-start justify-start gap-[15px] text-sm">
                  <div className="flex flex-row items-start justify-start gap-[5px]">
                    <img
                      className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px] z-[1]"
                      loading="lazy"
                      alt=""
                      src="/fluentemojiflatspiralcalendar1.svg"
                    />
                    <div className="flex flex-col items-start justify-start pt-px px-0 pb-0">
                      <b className="relative inline-block min-w-[110px] z-[1]">
                        5 березня 2024
                      </b>
                    </div>
                  </div>
                  <div className="flex flex-row items-start justify-start gap-[5px]">
                    <img
                      className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px] z-[1]"
                      loading="lazy"
                      alt=""
                      src="/fluentemojiflatalarmclock.svg"
                    />
                    <div className="flex flex-col items-start justify-start pt-px px-0 pb-0">
                      <b className="relative inline-block min-w-[39px] z-[1]">
                        12.30
                      </b>
                    </div>
                  </div>
                </div>
              </div>
              <div className="relative text-sm z-[1]">
                Анна Волошина, Галина Карпінська
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="flex flex-row items-start justify-start py-0 px-px box-border max-w-full">
        <div className="flex flex-row items-start justify-start gap-3.5 max-w-full mq450:flex-wrap">
          <div className="flex flex-col items-start justify-start gap-[15px]">
            <div className="relative inline-block min-w-[52px] z-[1]">
              Статус:
            </div>
            <div className="flex flex-row items-start justify-start py-0 pl-px pr-0">
              <div className="relative z-[1]">Посилання і документи:</div>
            </div>
            <div className="flex flex-row items-start justify-start py-0 px-px">
              <div className="relative z-[1]">Коментарі і нотатки:</div>
            </div>
          </div>
          <div className="flex flex-col items-start justify-start gap-[15px]">
            <div className="relative inline-block min-w-[81px] z-[1]">
              Планується
            </div>
            <div className="relative [text-decoration:underline] inline-block min-w-[85px] z-[1]">
              Презентація
            </div>
            <div className="relative z-[1]">Підготувати референси</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GroupComponent;
