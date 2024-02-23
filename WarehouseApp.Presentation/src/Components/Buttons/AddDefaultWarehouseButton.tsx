import { Button } from "antd";
import axios from "axios";

const AddDefaultWarehouseButton : React.FC<{onPost:() => void}> = ({onPost}) => {
    const handlePost = async () => {
        try{
            await axios.post(`https://localhost:7295/api/Square/fromFile`);
            onPost();
        }
        catch (error){
            console.error("Error deleting warehouse:", error);
        }
    };

    return (
        <Button type="primary" onClick={handlePost}>
            Add Default Warehouse
        </Button>
    )
}
export default AddDefaultWarehouseButton