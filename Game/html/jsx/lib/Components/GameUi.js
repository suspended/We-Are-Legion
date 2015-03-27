define(['lodash', 'react', 'react-bootstrap'], function(_, React, ReactBootstrap) {
    var Input = ReactBootstrap.Input;
    var OverlayTrigger = ReactBootstrap.OverlayTrigger;
    var Popover = ReactBootstrap.Popover;
    var Button = ReactBootstrap.Button;

    var pos = function(x, y, type) {
        if (typeof type === 'undefined') {
            type = 'absolute';
        }
    
        return {
            position:type,
            left: x + '%',
            top: y + '%',
        };
    };

    var size = function(x, y) {
        return {
            width: x + '%',
            height: y + '%',
        };
    };

    var width = function(x) {
        return size(x, 100);
    };

    var Div = React.createClass({
        render: function() {
            var style = {
                width:'100%',
                height:'100%',
            };
            
            style = _.assign(style, this.props.pos, this.props.size);
        
            return (
                <div style={style}>
                    {this.props.children}
                </div>
            );
        }
    });

    var RenderAtMixin = {
        render: function() {
            if (this.props.pos) {
                return (
                    <Div {...this.props}>
                        {this.renderAt()}
                    </Div>
                );
            } else {
                return this.renderAt();
            }
        }
    };

    var UiButton = React.createClass({
        mixins: [RenderAtMixin],
        
        renderAt: function() {
            var image = this.props.image;
            image.aspect = image.height / image.width;
            
            var width = this.props.width;
            var height = width * image.aspect;
            
            var button = <button className="UiButton" style={{backgroundImage: 'url('+image.url+')'}} />;
            
            var body;
            if (this.props.overlay) {
                body = 
                    <OverlayTrigger placement="top" overlay={this.props.overlay} delayShow={300} delayHide={150}>
                        {button}
                    </OverlayTrigger>
            } else {
                body = button;
            }
            
            return (
                <div style={{width:width+'%', height:0, paddingBottom:height+'%', position:'relative', 'float':'left'}}>
                    {body}
                </div>
            );
        }
    });

    var UiImage = React.createClass({
        mixins: [RenderAtMixin],
    
        renderAt: function() {
            var image = this.props.image;
            image.aspect = image.height / image.width;
            
            var width = this.props.width;
            var height = width * image.aspect;
            
            var style = {backgroundImage: 'url('+image.url+')'};
            style = _.assign(style, this.props.style);
            
            var img = <div className="UiImage" style={style} />;
            
            var body;
            if (this.props.overlay) {
                body = 
                    <OverlayTrigger placement="top" overlay={this.props.overlay} delayShow={300} delayHide={150}>
                        {img}
                    </OverlayTrigger>
            } else {
                body = img;
            }
            
            return (
                <div style={{width:width+'%', height:0, paddingBottom:height+'%', position:'relative', 'float':'left'}}>
                    {body}
                </div>
            );
        }
    });
    
    var ActionButton = React.createClass({
        mixins: [RenderAtMixin],
        
        renderAt: function() {
            return (
                <div>
                    <UiButton width={7} image={{width:160, height:160, url:'css/UiButton.png'}} {...this.props} />
                </div>
            );
        },
    });

    var UnitBar = React.createClass({
        mixins: [RenderAtMixin],
        
        renderAt: function() {
            return (
                <div>
                    <UiImage width={100} image={{width:869, height:60, url:'css/UnitBar.png'}} />
                    <Div pos={pos(0,.92)}>
                        <Div pos={pos(2,0)}><p>100</p></Div>
                        <Div pos={pos(16,0)}><p>100</p></Div>
                        <Div pos={pos(30,0)}><p>100</p></Div>
                    </Div>
                </div>
            );
        },
    });

    var Minimap = React.createClass({
        mixins: [RenderAtMixin],
        
        renderAt: function() {
            return (
                <div>
                    <UiImage pos={pos(0,0)} width={100} image={{width:245, height:254, url:'css/Minimap.png'}} />
                    <UiImage pos={pos(3.5,.35)} width={91} image={{width:245, height:254, url:'css/FakeMinimap.png'}} style={{position:'absolute',left:0,top:0,visibility:'hidden'}}/>
                </div>
            );
        },
    });

    var ChatInput = React.createClass({
        mixins: [RenderAtMixin],
        
        getInitialState: function() {
            return {
                value: '',
            }
        },
    
        onTextChange: function() {
            this.setState({
                value:this.refs.input.getValue()
            });
        },

        renderAt: function() {
            return (
                <Input value={this.state.value} ref="input" type="text" addonBefore="All" onChange={this.onTextChange} />
            );
        },
    });

    var UnitBox = React.createClass({
        mixins: [RenderAtMixin],
        
        renderAt: function() {
            return (
                <div>
                    <UiImage pos={pos(0,0)} width={100} image={{width:502, height:157, url:'css/UnitBox.png'}} />
                </div>
            );
        },
    });

    var Gap = React.createClass({
        render: function() {
            return (
                <div style={{'float':'left','overflow':'hidden','height':'1px',width:this.props.width + '%',}} />
            );
        }
    });
    
    return React.createClass({        
        render: function() {
            var tooltip = <Popover title="Fireball"><strong>FIRE!</strong> Check this info.</Popover>;
        
            return (
                <div>
                    <UnitBar pos={pos(50.5,.4)} size={width(50)} />
                    <Minimap pos={pos(.2,79)} size={width(11)} />

                    <Div pos={pos(15,0)}>
                        <ChatInput pos={pos(.35,80)} size={width(49)} />
                        
                        <Div pos={pos(0,85)}>
                            <ActionButton overlay={tooltip}/>
                            <ActionButton />
                            <ActionButton />
                            <ActionButton />
                            
                            <Gap width='1' />
                            
                            <ActionButton />
                            <ActionButton />
                            <ActionButton />
                        </Div>
                        
                        <UnitBox pos={pos(60,85)} size={width(23)} />
                    </Div>
                </div>
            );
        }
    });
});