import React from 'react';
import * as ArticlesModel from './model/articles';

import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import TextField from '@material-ui/core/TextField';

export default class extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            articles: [],
            modal:  {
                open: false,
                index: null,
                title: '',
                content: '',
                errors: []
            }
        };

        ArticlesModel.all().then((articles) => {
            this.setState({articles});
        });

        this.inpTitle = React.createRef();
        this.inpContent = React.createRef();
    }

    async onDelete(ind){
        let id = this.state.articles[ind].id;
        let response = await ArticlesModel.remove(id);

        if(response === true){
            let articles = [...this.state.articles];
            articles.splice(ind, 1);
            this.setState({articles});
        }
    }

    openModal(index, title, content){
        let modal = Object.assign({}, this.state.modal, {open: true, index, title, content, errors: []});
        this.setState({modal});
    }

    closeModal = () => {
        let modal = Object.assign({}, this.state.modal);
        modal.open = false;
        this.setState({modal});
    }

    onSave = () => {
        let form = {
            title: this.inpTitle.current.value,
            content: this.inpContent.current.value
        };

        if(this.state.modal.index === null){
            this.add(form);
        }
        else{
            this.edit(this.state.modal.index, form);
        }
    }

    async add(form){
        let response = await ArticlesModel.add(form);
        
        if(response.res === true){
            this.closeModal();
            let article = await ArticlesModel.one(response.id);
            let articles = [...this.state.articles];
            articles.push(article);
            this.setState({articles});
        }
        else{
            let modal = Object.assign({}, this.state.modal);
            modal.errors = response.errors;
            this.setState({modal});
        }
    }

    async edit(index, form){
        let id = this.state.articles[index].id;
        let response = await ArticlesModel.edit(id, form);

        if(response.res === true){
            this.closeModal();
            let article = await ArticlesModel.one(id);
            let articles = [...this.state.articles];
            articles[index] = article;
            this.setState({articles});
        }
        else{
            let modal = Object.assign({}, this.state.modal);
            modal.errors = response.errors;
            this.setState({modal});
        }
    }

    render(){
        let articles = this.state.articles.map((item, i) => {
            return <Grid item sm={3} key={item.id}>
                <Card>
                    <CardHeader title={item.title}></CardHeader>
                    <CardContent>{item.content}</CardContent>
                    <CardActions>
                        <Button variant="contained" color="primary" onClick={() => {
                            this.openModal(i, item.title, item.content)
                        }}>
                            Edit
                        </Button>
                        <Button variant="contained" color="secondary" onClick={() => this.onDelete(i)}>Delete</Button>
                    </CardActions>
                </Card>
            </Grid>
        });

        let errorsModal = this.state.modal.errors.map((item, i) => {
            return <p key={i}>{item}</p>
        });

        return <div>
                <Grid container spacing={32}>
                    {articles}
                </Grid>
                <hr/>
                <Button variant="contained" color="primary" onClick={() => this.openModal(null, '', '')}>
                    Add
                </Button>
                <Dialog open={this.state.modal.open} onClose={this.closeModal}>
                    <DialogContent>
                        <form>
                            <TextField
                                label="Post title"
                                defaultValue={this.state.modal.title}
                                margin="normal"
                                variant="outlined"
                                inputRef={this.inpTitle}
                            />
                            <br/>
                            <TextField
                                label="Post content"
                                multiline
                                rows={3}
                                defaultValue={this.state.modal.content}
                                margin="normal"
                                variant="outlined"
                                inputRef={this.inpContent}
                            />
                        </form>
                        {errorsModal}
                    </DialogContent>
                    <DialogActions>
                        <Button variant="contained" color="secondary" onClick={this.closeModal}>Cancel</Button>
                        <Button variant="contained" color="primary" onClick={this.onSave}>Save</Button>
                    </DialogActions>
                </Dialog>
            </div>;
    }   
}