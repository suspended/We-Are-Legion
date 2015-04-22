define(['lodash', 'react', 'interop', 'events',
        'Components/InGameUi', 'Components/GameLobby', 'Components/GameMenu', 'Components/InGameMenu', 'Components/OptionsMenu'],
    function(_, React,interop, events,
            InGameUi, GameLobby, GameMenu, InGameMenu, OptionsMenu) {
 
    return React.createClass({
        mixins: [],

        getInitialState: function() {
            return {
                screen:'game',
            };
        },

        render: function() {
            var body;
            //body = <GameMenu />;
            //body = <OptionsMenu />;
            //( )body = <CreateGame />;
            //( )body = <FindGame />;
            //body = <GameLobby host lobbyPlayerNum={2} />;
            //body = <GameLobby lobbyPlayerNum={2} />;
            body = React.createElement(InGameUi, null);
            //body = <InGameMenu />;

            return (
                React.createElement("div", null, 
                    body
                )
            );
        }
    });
});