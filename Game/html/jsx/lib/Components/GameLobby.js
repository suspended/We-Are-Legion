define(['lodash', 'react', 'react-bootstrap', 'interop', 'events', 'ui',
        'Components/Chat', 'Components/MapPicker'],
function(_, React, ReactBootstrap, interop, events, ui,
         Chat, MapPicker) {

    var Panel = ReactBootstrap.Panel;
    var Button = ReactBootstrap.Button;
    var Well = ReactBootstrap.Well;
    var Popover = ReactBootstrap.Popover;
    var Table = ReactBootstrap.Table;
    var ListGroup = ReactBootstrap.ListGroup;
    var ListGroupItem = ReactBootstrap.ListGroupItem;
    var ModalTrigger = ReactBootstrap.ModalTrigger;
    
    var Div = ui.Div;
    var Gap = ui.Gap;
    var UiImage = ui.UiImage;
    var UiButton = ui.UiButton;
    var Dropdown = ui.Dropdown;
    var OptionList = ui.OptionList;
    var RenderAtMixin = ui.RenderAtMixin;
    
    var pos = ui.pos;
    var size = ui.size;
    var width = ui.width;
    var subImage = ui.subImage;

    var make = function(english, chinese, value) {
        return ({
            name:
                <span>
                    {english}
                    <span style={{'text-align':'right', 'float':'right'}}>{chinese}</span>
                </span>,

            selectedName: english,
            value: value,
        });
    };

    var kingdomChoices = [
        make('Kingdom of Wei',   '魏', 1),
        make('Kingdom of Shu',   '蜀', 3),
        make('Kingdom of Wu',    '吳', 4),
        make('Kingdom of Beast', '獸', 2),
    ];

    var teamChoices = [
        make('Team 1', '一', 1),
        make('Team 2', '二', 2),
        make('Team 3', '三', 3),
        make('Team 4', '四', 4),
    ];

    var Choose = React.createClass({
        mixins: [],
                
        getInitialState: function() {
            var self = this;
            var selected = this.props.choices[0];

            _.forEach(this.props.choices, function(choice) {
                if (choice.value === self.props.value) {
                    selected = choice;
                }
            });

            return {
                selected: selected,
            };
        },
        
        onSelected: function(item) {
            console.log('something selected');
            if (interop.InXna()) {
                console.log('select team');
                xna.SelectTeam(this.props.player, item.value);
            }

            this.setState({
                selected: item,
            });
        },

        render: function() {
            if (this.props.activePlayer === this.props.info.SteamID) {
                return (
                    <Dropdown disabled={this.props.disabled}
                              selected={this.state.selected}
                              choices={this.props.choices}
                              onSelect={this.onSelected} />
                );
            } else {
                return (
                    <span>{this.state.selected.selectedName}</span>
                );
            }
        },
    });

    var PlayerEntry = React.createClass({
        mixins: [],
                
        getInitialState: function() {
            return {
            };
        },
        
        render: function() {
            if (this.props.info.Name) {
                return (
                    <tr>
                        <td>{this.props.info.Name}</td>
                        <td><Choose disabled={this.props.disabled} choices={teamChoices} value={this.props.info.GameTeam} default='Choose team' {...this.props} /></td>
                        <td><Choose disabled={this.props.disabled} choices={kingdomChoices} value={this.props.info.GamePlayer} default='Choose kingdom' {...this.props} /></td>
                    </tr>
                );
            } else {
                return (
                    <tr>
                        <td>Slot open</td>
                        <td></td>
                        <td></td>
                    </tr>
                );
            }
        },
    });

    return React.createClass({
        mixins: [events.LobbyMixin, events.LobbyMapMixin],

        onLobbyUpdate: function(values) {
            //values = {"SteamID":100410705,"LobbyName":"Cookin' Ash's lobby","Maps":['Beset', 'Clash of Madness', 'Nice'],"LobbyInfo":"{\"Players\":[{\"LobbyIndex\":0,\"Name\":\"Cookin' Ash\",\"SteamID\":100410705,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0}]}","LobbyLoading":false};
            //values = {"SteamID":100410705,"LobbyName":"Cookin' Ash's lobby","Maps":["Beset","Clash of Madness","Nice"],"LobbyLoading":true};
            //console.log('values');
            //console.log(JSON.stringify(values));
            //console.log('---');

            this.setState({
                loading: values.LobbyLoading || false,
                name: values.LobbyName || '',
                lobbyInfo: values.LobbyInfo ? JSON.parse(values.LobbyInfo) : null,
                activePlayer: values.SteamID,
                maps: values.Maps,
                map: 'Beset',
            });
        },

        onLobbyMapUpdate: function(values) {
            console.log('onLobbyMapUpdate');
            var mapLoading = values.LobbyMapLoading;

            if (mapLoading === this.state.mapLoading) {
                return;
            }

            this.setState({
                mapLoading: mapLoading,
            });
        },

        joinLobby: function() {
            if (!interop.InXna()) {
                values =
                    {"SteamID":100410705,"LobbyName":"Cookin' Ash's lobby","Maps":['Beset', 'Clash of Madness', 'Nice'],"LobbyInfo":"{\"Players\":[{\"LobbyIndex\":0,\"Name\":\"Cookin' Ash\",\"SteamID\":100410705,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0},{\"LobbyIndex\":0,\"Name\":null,\"SteamID\":0,\"GamePlayer\":0,\"GameTeam\":0}]}","LobbyLoading":false};

                setTimeout(function() {
                    window.lobby(values);
                }, 100);

                return;
            }

            if (this.props.params.host) {
                interop.createLobby();
            } else {
                interop.joinLobby(this.props.params.lobbyIndex);
            }
        },

        getInitialState: function() {
            this.joinLobby();

            return {
                loading: true,
                lobbyPlayerNum: 3,
                mapLoading: false,
            };
        },

        componentDidMount: function() {
            interop.drawMapPreviewAt(2.66, 0.554, 0.22, 0.22);
        },

        componentWillUnmount: function() {
            interop.hideMapPreview();
        },

        startGame: function() {
            if (interop.InXna()) {
                xna.StartGame();
            }
        },

        onClickStart: function() {
            this.setState({
                starting:true,
            });

            this.countDown();
        },

        countDown: function() {
            //this.startGame(); return;

            var _this = this;

            this.addMessage('Game starting in...');
            setTimeout(function() { _this.addMessage('3...'); }, 1000);
            setTimeout(function() { _this.addMessage('2...'); }, 2000);
            setTimeout(function() { _this.addMessage('1...'); }, 3000);
            setTimeout(_this.startGame, 4000);
        },

        addMessage: function(msg) {
            if (this.refs.chat && this.refs.chat.onChatMessage) {
                this.refs.chat.onChatMessage({message:msg,name:''});
            }
        },

        onMapPick: function(map) {
            console.log(map);
            interop.setMap(map);
        },

        leaveLobby: function() {
            interop.leaveLobby();
            back();
        },

        render: function() {
            var _this = this;

            if (this.state.loading) {
                return (
                    <div>
                    </div>
                );
            }

            console.log('this.state.lobbyInfo');
            console.log(JSON.stringify(this.state.lobbyInfo));

            var visibility = [
                {name:'Public game', value:'public'},
                {name:'Friends only', value:'friend'},
                {name:'Private', value:'private'},
            ];

            var disabled = this.state.starting;
            var preventStart = this.state.starting || this.state.mapLoading;

            return (
                <div>
                    <Div nonBlocking pos={pos(10,5)} size={width(80)}>
                        <Panel>
                            <h2>
                                {this.state.name}
                            </h2>
                        </Panel>

                        <Well style={{'height':'75%'}}>

                            {/* Chat */}
                            <Chat.ChatBox ref='chat' show full pos={pos(2, 17)} size={size(43,61)}/>
                            <Chat.ChatInput show lobbyChat pos={pos(2,80)} size={width(43)} />

                            {/* Player Table */}
                            <Div nonBlocking pos={pos(48,16.9)} size={width(50)} style={{'pointer-events':'auto', 'font-size': '1.4%;'}}>
                                <Table style={{width:'100%'}}><tbody>
                                    {_.map(_.range(1, 5), function(i) {
                                        return (
                                            <PlayerEntry disabled={disabled}
                                                         player={i}
                                                         info={_this.state.lobbyInfo.Players[i-1]}
                                                         activePlayer={_this.state.activePlayer} />
                                         );
                                    })}
                                </tbody></Table>
                            </Div>

                            {/* Map */}
                            <Div nonBlocking pos={pos(38,68)} size={width(60)}>
                                <div style={{'float':'right', 'pointer-events':'auto'}}>
                                    <p>
                                        {this.props.params.host ? 
                                            <ModalTrigger modal={<MapPicker maps={this.state.maps} onPick={this.onMapPick} />}>
                                                <Button disabled={disabled} bsStyle='primary' bsSize='large'>
                                                    Choose map...
                                                </Button>
                                            </ModalTrigger>
                                            : null}
                                    </p>
                                </div>
                            </Div>

                            {/* Game visibility type */}
                            {this.props.params.host ? 
                                <Div pos={pos(48,43)} size={size(24,66.2)}>
                                    <OptionList disabled={disabled} options={visibility} />
                                </Div>
                                : null}

                            {/* Buttons */}
                            <Div nonBlocking pos={pos(38,80)} size={width(60)}>
                                <div style={{'float':'right', 'pointer-events':'auto'}}>
                                    <p>
                                        {this.props.params.host ?
                                            <Button disabled={preventStart} onClick={this.onClickStart}>Start Game</Button>
                                            : null}
                                        &nbsp;
                                        <Button disabled={disabled} onClick={this.leaveLobby}>Leave Lobby</Button>
                                    </p>
                                </div>
                            </Div>

                        </Well>
                    </Div>
                </div>
            );
        }
    });
}); 