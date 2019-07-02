import Moment from 'moment';

export const toSqlFormat = (date) => {
    return Moment(date).format('YYYY-MM-DD HH:mm:ss');
}