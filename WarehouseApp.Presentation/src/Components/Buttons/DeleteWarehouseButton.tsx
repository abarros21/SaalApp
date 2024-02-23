import { Button } from "antd";
import axios from "axios";

const DeleteWarehouseButton : React.FC<{warehouseKey: string; onDelete:() => void}> = ({warehouseKey, onDelete}) => {
    const handleDelete = async () => {
        try{
            await axios.delete(`https://localhost:7295/api/Square/${warehouseKey}`);
            onDelete();
        }
        catch (error){
            console.error("Error deleting warehouse:", error);
        }
    };

    return (
        <Button type="primary" danger onClick={handleDelete}>
            Remove Warehouse
        </Button>
    )
}

export default DeleteWarehouseButton