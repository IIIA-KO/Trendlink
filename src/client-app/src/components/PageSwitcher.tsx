import React, { useState } from 'react';

const Menu = () => {
  const [activeIndex, setActiveIndex] = useState(0);

  const menuItems = [
    { name: 'Сторінка', link: '/page' },
    { name: 'Відео', link: '/video' },
    { name: 'Сервіси', link: '/services' },
    { name: 'Навчання', link: '/training' },
    { name: 'Усі сервіси', link: '/all-services' }
  ];

  return (
    <ul className="menu">
      {menuItems.map((item, index) => (
        <li
          key={index}
          className={`menu-item ${index === activeIndex ? 'active' : ''}`}
          onClick={() => setActiveIndex(index)}
        >
          {item.name}
        </li>
      ))}
    </ul>
  );
};

export default Menu;