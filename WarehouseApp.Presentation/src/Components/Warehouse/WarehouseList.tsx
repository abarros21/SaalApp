import axios from "axios";
import { useEffect, useState } from "react";
import { Flex, Layout, Result } from "antd";
import { Content, Footer, Header } from "antd/es/layout/layout";

import WarehouseMap from "./WarehouseMap";
import AddDefaultWarehouseButton from "../Buttons/AddDefaultWarehouseButton";

import {contentStyle, headerStyle, layoutStyle} from "../../Styles/Warehouse/WarehouseListStyles"
import { Typography } from 'antd';

const { Title, Text } = Typography;

const WarehouseList: React.FC = () => {
  const [warehouses, setWarehouses] = useState<WarehouseDto[]>([]);
  const [isWarehouseEmpty, setIsWarehouseEmpty] = useState<boolean>(false);
  useEffect(() => {
    fetchWarehouses();
  }, []);

  const fetchWarehouses = async () => {
    try {
      const response = await axios.get<WarehouseDto[]>('https://localhost:7295/api/Square');
      console.log("Warehouses ", response.data);
      setWarehouses(response.data);
      setIsWarehouseEmpty(false);
    } catch (error) {
      if (error.response?.status == 404) {
        setIsWarehouseEmpty(true);
      }
      console.error('Error fetching warehouses:', error);
    }
  };
  const handleDeleteWarehouse = (): void => {
    fetchWarehouses();
  };
  const handlePostWarehouse = (): void => {
    fetchWarehouses();
  };

  return (
    <Flex gap ="middle" wrap="wrap">
      <Layout style={layoutStyle}>
        <Header style={headerStyle}>
          Warehouses
        </Header>
      <Content style={contentStyle}>
      {isWarehouseEmpty ?
        (
          <Result
            status="404"
            title="404"
            subTitle="No warehouses available"
            extra={<AddDefaultWarehouseButton
              onPost={handlePostWarehouse}
            />}
          />
        ) : (
          <div>            
            <ul>
              {warehouses.map((warehouse, index) => (
                <li key={index}>
                  <Title level={3}>Warehouse Key: <Text strong>{warehouse.key}</Text></Title>
                  <ul>
                    <WarehouseMap
                      warehouse={warehouse}
                      onDeleteWarehouse={handleDeleteWarehouse} />
                  </ul>
                </li>
              ))}
            </ul>
          </div>
        )}
        </Content>
        <Footer />
        </Layout>
    </Flex>
  );
};

export default WarehouseList