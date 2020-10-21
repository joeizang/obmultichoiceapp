import React, {FC} from 'react';
import { Container } from 'react-bootstrap';


interface DashboardProps {
    
}

const SalesDashboard: FC<DashboardProps> = () => {
    return(
        <>
            <Container>
                <h3>Sales DashBoard!</h3>
            </Container>
        </>
    )
}

export default SalesDashboard;