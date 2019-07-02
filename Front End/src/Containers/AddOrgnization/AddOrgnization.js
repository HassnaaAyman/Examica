import React, { Component } from 'react';
import './AddOrgnization.css';
import { connect } from 'react-redux';
import { Form, Button, Input, Select} from 'element-react/next';
import { addOrg } from '../../Store/Actions/organizationActions';

class AddOrgnization extends Component {
	constructor(props) {
		super(props);

		this.state = {
			form: {
				Name: '',
				PricingPlanId: '',
				OwnerId: '',
				Image: ''
			},
			rules: {
				Name: [{ required: true, message: 'Please input Organization name', trigger: 'blur' }],
				PricingPlanId: [{ required: true, type: "number", message: 'Please Select a Pricing Plan', trigger: 'blur' }]
			},
			pricingPlans: [{ Id: 2, Name: 'Basic' }, { Id: 3, Name: 'Pro' }, { Id: 4, Name: 'Pro +' }]
		};
	}

	handleSubmit(e) {
		e.preventDefault();

		this.refs.form.validate((valid) => {
			if (valid) {
				var org = {...this.state.form};
				org.OwnerId = this.props.userId;
				this.props.onAdd(org,this.props.token);
				this.props.history.push("/");
			} else {
				return false;
			}
		});
	}

	handleReset(e) {
		e.preventDefault();
		this.refs.form.resetFields();
		this.setState({
			form: {
				Name: '',
				PricingPlanId: '',
				Image: ''
			}
		});
	}

	onChange(key, value) {
		if(key === "PricingPlanId") value = Number(value);
		this.setState({
			form: Object.assign({}, this.state.form, { [key]: value })
		});
	}

	render() {
		return (
			<Form
				ref="form"
				className="en-US AddOrgnization"
				model={this.state.form}
				labelWidth="120"
				rules={this.state.rules}
				onSubmit={this.handleSubmit.bind(this)}
			>
				<div className="title">Register a new Organization</div>
				<Form.Item label="Name" prop="Name"><Input value={this.state.form.Name} onChange={this.onChange.bind(this, 'Name')} />
				</Form.Item>
				<Form.Item label="Pricing Plan" prop="PricingPlanId">
					<Select
						value={this.state.form.PricingPlanId}
						onChange={this.onChange.bind(this, 'PricingPlanId')}
						placeholder="Please select a Plan"
					>
						{this.state.pricingPlans.map((item) => (
							<Select.Option key={item.Id} label={item.Name} value={item.Id} />
						))}
					</Select>
				</Form.Item>

				{/* <Form.Item label="Image URL" prop="Image">
					<Input value={this.state.form.Image} onChange={this.onChange.bind(this, 'Image')} />
				</Form.Item> */}

				<Form.Item>
					<Button type="primary" nativeType="submit" onClick={this.handleSubmit.bind(this)}>
						submit
					</Button>
					<Button onClick={this.handleReset.bind(this)}>Reset</Button>
				</Form.Item>
			</Form>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		token: state.auth.token,
		userId: state.auth.userId,
		isLoading: state.isLoading
	};
};

const mapDispatchToProps = dispatch => {
	return {
			onAdd: (org, token) => dispatch(addOrg(org,token))
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(AddOrgnization);
