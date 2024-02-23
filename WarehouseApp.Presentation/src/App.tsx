import WarehouseList from "./Components/Warehouse/WarehouseList";

import { Typography } from 'antd';

const { Title } = Typography;

const App = () => {
  return (
    <div>
      <Title>Warehouse Management System</Title>
      <WarehouseList />
    </div>
  );
};


export default App
