import { Group, Layer, Rect, Stage, Text } from "react-konva";
import DeleteWarehouseButton from "../Buttons/DeleteWarehouseButton";

interface WarehouseMapComponentProps {
    warehouse: WarehouseDto;
    onDeleteWarehouse : () => void;
  }

  const squareSize = 50;

const WarehouseMap: React.FC<WarehouseMapComponentProps> = ({ warehouse, onDeleteWarehouse  }) => {
    const squares = warehouse.squares;

    const maxX = squares.reduce((max, square) => Math.max(max, square.x), 0);
    const maxY = squares.reduce((max, square) => Math.max(max, square.y), 0);

    const width = (maxX) * squareSize;
    const height = (maxY) * squareSize;
    return (
      <>
        <Stage width={width} height={height}>
          <Layer>
            <Rect
              x={0}
              y={0}
              width={width}
              height={height}
              fill="white"
              stroke="black"
              strokeWidth={3}
            />            
            {squares.map(square => (
              <Group key={`${square.x}-${square.y}`}>
                <Rect
                  key={`${square.x}-${square.y}`}
                  x={(square.x -1) * squareSize}
                  y={height - square.y * squareSize}
                  width={squareSize}
                  height={squareSize}
                  fill={square.distance==0 ? "yellow" : "blue"}
                  stroke="black"
                  strokeWidth={1}
                />
                {square.distance > 0 && 
                <Text
                  x={(square.x - 1) * squareSize + 20} 
                  y={height - square.y * squareSize + 20} 
                  text={square.distance.toString()} 
                  fill="black" 
                  fontSize={22}                             
                  fontWeight="bold"
                  />}
               
              </Group>              
            ))}
          </Layer>
        </Stage>
         <DeleteWarehouseButton 
         warehouseKey={warehouse.key} 
         onDelete={onDeleteWarehouse}                
       />
       </>
      );
}

export default WarehouseMap