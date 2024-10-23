import { Checkbox } from 'antd';
import React, { useState } from 'react';

const TableNotifications = () => {
  const [data, setData] = useState([
    {
      id: 1,
      name: 'Inna_Cozub',
      description: 'Inna_Co/стиль/краса...',
      adType: 'Сторіс в Instagram',
      date: '28.04.2024',
      price: '300$',
      status: 'В обробці',
      imgSrc: 'https://via.placeholder.com/40x40'
    },
    // Add more data as needed
  ]);

  return (
    <div className="w-96 l-30 h-96 flex-col justify-start items-start inline-flex">
      {data.map((item) => (
        <div key={item.id} className="self-stretch border-b border-stone-300 justify-start items-center inline-flex">
          <div className="h-14 px-4 pt-1.5 pb-2 border-r border-stone-300 justify-start items-center gap-2.5 flex">
            <div className="grow shrink basis-0 h-10 justify-start items-center gap-12 flex">
              <Checkbox></Checkbox>
              <div className="grow shrink basis-0 h-10 justify-start items-center gap-2.5 flex">
                <div className="w-10 h-10 justify-center items-center flex">
                  <img className="w-10 h-10 rounded-full" src={item.imgSrc} alt={item.name} />
                </div>
                <div className="grow shrink basis-0 flex-col justify-start items-start inline-flex">
                  <div className="text-neutral-700 text-sm font-normal font-['Inter'] leading-none">{item.name}</div>
                  <div className="text-zinc-600 text-xs font-normal font-['Inter']">{item.description}</div>
                </div>
              </div>
            </div>
          </div>
          <div className="w-36 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-xs font-normal font-['Inter'] leading-none">{item.adType}</div>
          </div>
          <div className="w-28 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-sm font-normal font-['Inter']">{item.date}</div>
          </div>
          <div className="w-28 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-sm font-normal font-['Inter']">{item.price}</div>
          </div>
          <div className="w-44 px-4 py-3.5 border-r border-stone-300 flex-col justify-center items-center gap-2.5 inline-flex">
            <div className="w-32 h-6 justify-center items-center inline-flex">
              <div className="w-32 h-6 py-2.5 bg-blue-50 rounded-3xl justify-center items-center inline-flex">
                <div className="text-neutral-700 text-xs font-normal font-['Inter']">{item.status}</div>
              </div>
            </div>
          </div>
        </div>
      ))}
        {data.map((item) => (
        <div key={item.id} className="self-stretch border-b border-stone-300 justify-start items-center inline-flex">
          <div className="h-14 px-4 pt-1.5 pb-2 border-r border-stone-300 justify-start items-center gap-2.5 flex">
            <div className="grow shrink basis-0 h-10 justify-start items-center gap-12 flex">
              <Checkbox></Checkbox>
              <div className="grow shrink basis-0 h-10 justify-start items-center gap-2.5 flex">
                <div className="w-10 h-10 justify-center items-center flex">
                  <img className="w-10 h-10 rounded-full" src={item.imgSrc} alt={item.name} />
                </div>
                <div className="grow shrink basis-0 flex-col justify-start items-start inline-flex">
                  <div className="text-neutral-700 text-sm font-normal font-['Inter'] leading-none">{item.name}</div>
                  <div className="text-zinc-600 text-xs font-normal font-['Inter']">{item.description}</div>
                </div>
              </div>
            </div>
          </div>
          <div className="w-36 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-xs font-normal font-['Inter'] leading-none">{item.adType}</div>
          </div>
          <div className="w-28 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-sm font-normal font-['Inter']">{item.date}</div>
          </div>
          <div className="w-28 self-stretch px-4 py-5 border-r border-stone-300 justify-center items-center gap-2.5 flex">
            <div className="text-neutral-700 text-sm font-normal font-['Inter']">{item.price}</div>
          </div>
          <div className="w-44 px-4 py-3.5 border-r border-stone-300 flex-col justify-center items-center gap-2.5 inline-flex">
            <div className="w-32 h-6 justify-center items-center inline-flex">
              <div className="w-32 h-6 py-2.5 bg-blue-50 rounded-3xl justify-center items-center inline-flex">
                <div className="text-neutral-700 text-xs font-normal font-['Inter']">{item.status}</div>
              </div>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default TableNotifications;
