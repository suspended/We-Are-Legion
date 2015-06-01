define(['lodash', 'react', 'react-bootstrap'], function(_, React, ReactBootstrap) {
    var MenuItem = ReactBootstrap.MenuItem;

    return React.createClass({
        mixins: [],
                
        getInitialState: function() {
            return {
            };
        },
        
        onSelect: function() {
            if (this.props.onSelect) {
                this.props.onSelect(this.props.item);
            }
        },

        render: function() {
            var className = null;
            if (this.props.disabled) {
                className = 'disabled-item';
            }

            return (
                <MenuItem className={className} onClick={this.onSelect} width='1000px'>
                    {this.props.name}
                </MenuItem>
            );
        },
    });
});