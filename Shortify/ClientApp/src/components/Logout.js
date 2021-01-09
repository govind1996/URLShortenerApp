import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { startLogout } from '../actions/auth';

class Logout extends React.Component {
    logout= () =>{
        this.props.onLogout(this.props.token);
    }
    render(){
        this.logout();
        return(
            <Redirect to='/' />
        )
    }
}
const mapStateToProps = (state) => ({
    token: state.auth.authToken
})
const mapDispatchToProps = (dispatch) => ({
    onLogout: (token) => dispatch(startLogout(token)) 
})

export default connect(mapStateToProps, mapDispatchToProps)(Logout)